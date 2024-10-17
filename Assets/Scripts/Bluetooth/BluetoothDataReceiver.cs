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
    SagittalControllerScript sagittalController;
    TranverseControllerScript tranverseController;
    
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
        sagittalController = FindObjectOfType<SagittalControllerScript>();
        Debug.Log("Found sagittal: " + sagittalController);
        tranverseController = FindObjectOfType<TranverseControllerScript>();
        Debug.Log("Found sagittal: " + tranverseController);


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

    /// <summary>
    /// Sagittal bone movement visualizer with denominator (for limit force range) and threshold (maximum movement)
    /// </summary>
    /// <param name="denominator"> denominator to reduce range of force </param>
    /// <param name="threshold"> maximum movement value </param>
    private void sagittalBonesMovement(float denominator, float threshold)
    {
        sagittalController.resetPosition(); // reset position 
        for (int i = 0; i < sectional_change.Length; i++)
        {
            float value = computeSectionForce(i)/denominator;
            if (Math.Abs(value) > threshold)
            {
                value = threshold;
            }
            value = (float)Math.Round(value, 1);
            sagittalController.bones[i+1].moveDown(value);
        }
    }


    /// <summary>
    /// Perform tranverse bone movement visualisation given section index and force
    /// </summary>
    /// <param name="focusSectionIndex"></param>
    private void tranverseBonesMovementVisualiser(int focusSectionIndex)
    {
        float MAX_ANGLE = 0.1f;
        //float leftDepth = this.sensors_displacements[focusSectionIndex * 2 + 0];
        //float rightDepth = this.sensors_displacements[focusSectionIndex * 2 + 1];        
        //float halfDistance = Math.Abs(leftDepth - rightDepth)/2;
        //float rotateAngle = 0;
        //int boneLength = 50;



        //if (halfDistance > 0.02)
        //{
        //    if (leftDepth == 0 || rightDepth == 0)
        //    {
        //        rotateAngle = Mathf.Sin(leftDepth / boneLength);
        //    }
        //    else
        //    {
        //        rotateAngle = Mathf.Sin(halfDistance / boneLength);
        //    }

        //    if (leftDepth < rightDepth)
        //    {
        //        //bone.transform.localRotation = Quaternion.Euler(0f, -rotateAngle *500f, 0f);
        //        //UnityDebug.Log("----origin: " + originalDegree + ", rotateAngle: " + -rotateAngle);
        //        rotateAngle = -rotateAngle;
        //    }

        //    tranverseController.rotate(focusSectionIndex, rotateAngle);
        //}




        float leftForce = computeSensorForce(focusSectionIndex * 2 + 0);
        float rightForce = computeSensorForce(focusSectionIndex * 2 + 1);
        // If there is not much different on force applying on both side, ignore 
        if (Math.Abs(leftForce - rightForce) <= 1)
        {
            return;
        }


        float force = Math.Min(17, Math.Max(leftForce, rightForce)); // upper constrant 
        if (force >= 5)
        {
            float angle = MAX_ANGLE * force / 17f;
            if (leftForce > rightForce)
            {
                angle = -angle;
            }

            tranverseController.rotate(focusSectionIndex, angle);
        }
    }


    private void sagittalBoneMovementV2(int focusSectionIndex, float force, float threshold)
    {
        sagittalController.resetPosition();
        force = force / threshold * 0.5f;
        if (force < 0.1)
        {
            return;
        }
        sagittalController.moveCurveBone(force, focusSectionIndex);
        //if (Math.Abs(force) >= threshold)
        //{
        //    sagittalController.moveCurveBone(0.5f, focusSectionIndex); // option 2: move with only caring about engaged section
        //} else if (Math.Abs(force) >= 0.3)
        //{
        //    sagittalController.moveCurveBone(0.2f, focusSectionIndex);
        //}
    }

    //public override void setDelayTime(float delayTime)
    //{
    //    this.delayTime = delayTime;
    //}








    // adjust the 0.5 of thingy for option 2, also thinkabout the skipped section
    // option 1: set the boundaries 


}
