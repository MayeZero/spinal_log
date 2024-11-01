using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BluetoothDataReceiver : BluetoothReceiverSuperClass
{
    [SerializeField] ShowCustomGraphYT graphYT;
    [SerializeField] UISwitcher.UISwitcher toggle;
    //private BluetoothManager bluetoothManager;
    
    [SerializeField] Text output;
    [SerializeField] Text log;
    [SerializeField] Text change;

    public string sensorDatainString;
    //public bool connected = false;

    //public float[] converted_data = new float[8];
    public float[] sensors_loads1 = new float[8];
    public float[] sensors_loads2 = new float[8];
    public float[] sensors_displacements = new float[8];
    public int numSensors;
    public int numSections;
    public float[] sectional_displacements = new float[4];
    public float[] sectional_change = new float[4];
    public float[] sectional_loads = new float[4];
    private int focusSectionIndex = 0;
    public string input;
    public float focusSectionForce;
    public int denominator = 1;
    private MonoBoneScript[] bones = new MonoBoneScript[4];
    //[SerializeField] float delayTime = 0.08f;

    // This field controls the low pass value
    // Use 1 for no filtering, and a value closer to zero for more sluggish filtering 
    // (Note that zero would be invalid and freeze the transform)
    //
    //[Range(0.1f, 1.0f)]
    ////public float lowPassFilter = 0.5f;
    //public float LowPassFilter { set { lowPassFilter = value; } }
    //public float currentData, targetData;

    private IEnumerator myCoroutine;

    void Start()
    {

        bluetoothManager = FindObjectOfType<BluetoothManager>();
        Debug.Log("found bluetooth manager: " + bluetoothManager);
        connected = true;
        
        toggle.isOn = bluetoothManager.IsStiff;
        numSections = 4;
        numSensors = 8;
        focusSectionForce = currentData = targetData = 0.0f;
        output.text = bluetoothManager.inputdata;

        for (int i = 0; i < sensors_loads1.Length; i++)
        {
            sensors_loads1[i] = 0.0f;
            sensors_displacements[i] = 0.0f;
        }

        for (int i = 0; i < sectional_loads.Length; i++)
        {
            sectional_loads[i] = 0.0f;
            sectional_displacements[i] = 0.0f;
            sectional_change[i] = 0.0f;
        }

        focusSectionIndex = 0;

        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine = DataProcessing(delayTime);
        StartCoroutine(myCoroutine);

    }

    private IEnumerator DataProcessing(float waitTime)
    {
        while (true)
        {
        //output.text = bluetoothManager.inputdata;

        sensorDatainString = bluetoothManager.inputdata;
        
        converted_data = ConvertedFloat(sensorDatainString);  // convert string signal to float 
        computeDisplacementWithDistance(); // fill sensors_displacement 
        findSectionEngaged();  // fill sectional_sensor and section_change 


        // sectional displacement change
        Debug.Log("sectional displacement: " + sectional_displacements);
        //sagittalBonesMovement(30, 0.5f); // option 1: move with mapping value to each bone section

        this.focusSectionForce = computeSectionForce(focusSectionIndex); // compute force for graph visualisation 
        // sagittalBoneMovementV2(focusSectionIndex, focusSectionForce, 17f); // still option 2, but with curation
        // tranverseBonesMovementVisualiser(focusSectionIndex);
        

        // ===== Low-pass filter here ====== // 
        if (focusSectionForce != currentData)
        {
            targetData = focusSectionForce;
        }

        if (lowPassFilter < 1)
        {
            focusSectionForce = Mathf.Lerp(currentData, targetData, lowPassFilter);
        }

        currentData = focusSectionForce;
        // ================================ //

        graphYT.addRealTimeDataToGraph(focusSectionForce);
        string data = focusSectionForce.ToString();//testing 
        
        output.text = "Focused Section: " + focusSectionIndex;
        log.text = "Force: " + focusSectionForce;
        //change.text = "Change: " + sectional_change[focusSectionIndex];

        sensors_loads1 = converted_data;

        yield return new WaitForSeconds(waitTime);
        }
        
    }

    private void Update()
    {
        if (bluetoothManager != null && connected)
        {
            sensorDatainString = bluetoothManager.inputdata;
            converted_data = ConvertedFloat(sensorDatainString);
        }

    }

    private float[] ConvertedFloat(string inputData)
    {
        float[] convertData = new float[8]; 
        convertData = Array.ConvertAll(inputData.Split(','), float.Parse);
        return convertData;
    }

    private void computeVertebraePoses()
    {
        //Computing sections displacements and finding the section with the largest displacement in the same loop
        //this.focusSectionIndex = 0;

        for (int i = 0; i < this.numSections; i++)
        {
            int indexLeft = i * 2 + 0;
            int indexRight = i * 2 + 1;
            //////////////////////////////////////////////////////////////////////////////
            this.sectional_displacements[i] = (this.sensors_displacements[indexLeft] + this.sensors_displacements[indexRight]) / 2; //this algorithm may need revision
            //////////////////////////////////////////////////////////////////////////////
            if (Math.Abs(this.sectional_displacements[i]) > Math.Abs(this.sectional_displacements[this.focusSectionIndex]))
            {
                this.focusSectionIndex = i;
            }
        }

       
    }
    private void computeVertebraeLoads()
    {
        for (int i = 0; i < this.numSections; i++)
        {
            int indexLeft = i * 2 + 0;
            int indexRight = i * 2 + 1;
            //////////////////////////////////////////////////////////////////////////////
            this.sectional_loads[i] = (this.sensors_loads2[indexLeft] + this.sensors_loads2[indexRight]) / 2; //this algorithm may need revision
            //////////////////////////////////////////////////////////////////////////////
        }
    }

    private void computeDisplacementWithDistance()
    {
        for (int i = 0; i < sensors_displacements.Length; i++)
        {
            this.sensors_displacements[i] = this.converted_data[i] - this.sensors_loads1[i];
        }
    }

    private void findSectionEngaged()
    {
        

        for (int i = 0; i < this.numSections; i++)
        {
            int indexLeft = i * 2 + 0;
            int indexRight = i * 2 + 1;
            //////////////////////////////////////////////////////////////////////////////
            float previousValue = this.sectional_displacements[i];
            this.sectional_displacements[i] = (this.sensors_displacements[indexLeft] + this.sensors_displacements[indexRight]) / 2; //this algorithm may need revision
            this.sectional_change[i] = this.sectional_displacements[i] - previousValue;
            //////////////////////////////////////////////////////////////////////////////
            if (Math.Abs(this.sectional_displacements[i]) > Math.Abs(this.sectional_displacements[this.focusSectionIndex]))
            {
                this.focusSectionIndex = i;
            }
        }
    }

    private float computeSensorForce(int sensorIndex)
    {
        ///Kiichi
        if(bluetoothManager.IsStiff){
            float sensorForce = (1 - this.converted_data[sensorIndex] / 40) * 146;
            
            return sensorForce;
        } else{
            float sensorForce = (1 - this.converted_data[sensorIndex] / 30) * 219;
            
            return sensorForce;
        }

        ///Haining
        // if(bluetoothManager.IsStiff){
        //     float sensorForce = (1 - this.converted_data[sensorIndex] / 35) * 170.3f;
        //     return sensorForce;
        // } else{
        //     float sensorForce = (1 - this.converted_data[sensorIndex] / 43) * 136.48f;
        //     return sensorForce;
        // }


        
    }

    private float computeSectionForce(int focusSectionIndex)
    {
        float force = (computeSensorForce(focusSectionIndex * 2 + 0) + computeSensorForce(focusSectionIndex * 2 + 1)) / 2 - 5.0f;
        if (force <= 0.0f)
        {
            return 0.0f;
        }
        else if (force >= 30)
        {
            return 30.0f;
        }
        else
        {
            return force;
        }

    }




}
