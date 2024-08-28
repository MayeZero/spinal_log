using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BluetoothDataReceiver : MonoBehaviour
{
    [SerializeField] ShowCustomGraphYT graphYT;
    [SerializeField] UISwitcher.UISwitcher toggle;
    private BluetoothManager bluetoothManager;
    SagittalControllerScript sagittalController;
    TranverseControllerScript tranverseController;
    
    [SerializeField] Text output;
    [SerializeField] Text log;
    [SerializeField] Text change;

    public string sensorDatainString;

    public float[] converted_data = new float[8];
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
    

    private IEnumerator myCoroutine;

    void Start()
    {
        sagittalController = FindObjectOfType<SagittalControllerScript>();
        Debug.Log("Found sagittal: " + sagittalController);
        //bones[0] = sagittalController.L1Bone;

        
        bluetoothManager = FindObjectOfType<BluetoothManager>();
        Debug.Log("found bluetooth manager: " + bluetoothManager);
        
        toggle.isOn = bluetoothManager.IsStiff;
        numSections = 4;
        numSensors = 8;
        focusSectionForce = 0.0f;
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
        myCoroutine = DataProcessing(0.02f);
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
        sagittalBonesMovement(0.1f, 0.5f); // option 1: move with mapping value to each bone section

        this.focusSectionForce = computeSectionForce(focusSectionIndex); // compute force for graph visualisation 
        sagittalController.moveCurveBone(0.5f, focusSectionIndex); // option 2: move with only caring about engaged section
        graphYT.addRealTimeDataToGraph(focusSectionForce) ;

        string data = focusSectionForce.ToString();//testing 
        //string sensorsDisplacementsString = string.Join(", ", sensors_displacements);
        
        output.text = "Focused Section: " + focusSectionIndex;
        log.text = "Force: " + focusSectionForce;
        change.text = "Change: " + sectional_change[focusSectionIndex];

        sensors_loads1 = converted_data;

        yield return new WaitForSeconds(waitTime);
        }
        
    }

    private void Update()
    {
        
        
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
        float force = (computeSensorForce(focusSectionIndex * 2 + 0) + computeSensorForce(focusSectionIndex * 2 + 1)) / 2;
        if (force < 15.0f){
            force = 0.1f;
        }

        return force;

        
        
        //this.focusSectionForce = (computeSensorForce(focusSectionIndex * 2 + 0) + computeSensorForce(focusSectionIndex * 2 + 1)) / 2;
            
    }

    /// <summary>
    /// Sagittal bone movement visualizer with denominator (for limit force range) and threshold (maximum movement)
    /// </summary>
    /// <param name="denominator"> denominator to reduce range of force </param>
    /// <param name="threshold"> maximum movement value </param>
    private void sagittalBonesMovement(float denominator, float threshold)
    {
        for (int i = 0; i < sagittalController.bones.Length; i++)
        {
            float value = sectional_change[i] / denominator;
            if (Math.Abs(value) > threshold)
            {
                value = threshold;
            }
            value = (float)Math.Round(value, 2);
            sagittalController.bones[i].moveDown(value);
        }
    }


    private void tranverseBonesMovement()
    {

    }







    // adjust the 0.5 of thingy for option 2, also thinkabout the skipped section
    // option 1: set the boundaries 


}
