using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BluetoothDataRecieverPreset : BluetoothReceiverSuperClass
{
    [SerializeField] ShowingGraphPreset graph;

    //private BluetoothManager bluetoothManager;
    
    [SerializeField] Text output;
    [SerializeField] Text log;

    //public bool Available = false;
    public string sensorDatainString;

    //public float[] converted_data = new float[8];
    public float[] sensors_loads1 = new float[8];
    public float[] sensors_loads2 = new float[8];
    public float[] sensors_displacements = new float[8];
    public int numSensors;
    public int numSections;
    public float[] sectional_displacements = new float[4];
    public float[] sectional_loads = new float[4];
    private int focusSectionIndex;
    public string input;

    private IEnumerator myCoroutine2;
    public float focusSectionForce;
    private float initialAvgForce;

    void Start()
    {
        // Initialize variables and start data processing coroutine
        bluetoothManager = FindObjectOfType<BluetoothManager>();
        connected = true;

        numSections = 4;
        numSensors = 8;

        for (int i = 0; i < sensors_loads1.Length; i++)
        {
            sensors_loads1[i] = 0.0f;
            sensors_displacements[i] = 0.0f;
        }

        for (int i = 0; i < sectional_loads.Length; i++)
        {
            sectional_loads[i] = 0.0f;
            sectional_displacements[i] = 0.0f;
        }

        focusSectionForce = currentData = targetData = 0.0f;

        // sensorDatainString = bluetoothManager.inputdata;
        // converted_data = ConvertedFloat(sensorDatainString);
        // initialAvgForce = 

        if (myCoroutine2 != null)
        {
            StopCoroutine(myCoroutine2);
        }
        myCoroutine2 = DataProcessing(delayTime);
        Debug.Log("my coroutine set");
        StartCoroutine(myCoroutine2);
        Debug.Log("Coroutine2 started");

    }
    // Coroutine for continuous data processing 
    private IEnumerator DataProcessing(float waitTime)
    {

        while (true)
        {
        sensorDatainString = bluetoothManager.inputdata;
        
        converted_data = ConvertedFloat(sensorDatainString);

        computeDisplacementWithDistance();
        findSectionEngaged();
        focusSectionForce = computeSectionForce(focusSectionIndex);

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

        graph.addRealTimeDataToGraph(focusSectionForce);
        Debug.Log("Force preset: " + focusSectionForce);
        output.text = "Focused Section: " + focusSectionIndex;
        log.text = "Force: " + focusSectionForce;

        sensors_loads1 = converted_data;

        yield return new WaitForSeconds(waitTime);
        }
        
    }
    //Convert string input to float array
    private float[] ConvertedFloat(string inputData)
    {
        float[] convertData = new float[8]; 
        convertData = Array.ConvertAll(inputData.Split(','), float.Parse);
        return convertData;
    }
    //compute vertebrae poses (for 3d modelling)
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

        //////////////Computing individual vertebral displacement and orientation in both planes

        //for (int i = 0; i < numBones; i++)
        //{

        //    float vertebralDisp;
        //    float sagTilt;
        //    float heightsDiff;  //difference between left side and right side
        //    float transTilt;

        //    float leftDisp, rightDisp;

        //    if (i == 0)
        //    { //the first bone
        //        sagTilt = (float)((sectional_displacements[i] - sectional_displacements[i + 1]) * 0.05); ///0.05 random factor, tune it manually
        //    }
        //    else if (i == numBones - 1)
        //    { //the last bone
        //        sagTilt = (float)((sectional_displacements[i - 1] - sectional_displacements[i]) * 0.05);
        //    }
        //    else
        //    {
        //        sagTilt = (float)((sectional_displacements[i - 1] - sectional_displacements[i + 1]) * 0.05);
        //    }

        //    vertebralDisp = sectional_displacements[i];

        //    leftDisp = sensors_displacements[i * 2];
        //    rightDisp = sensors_displacements[(i * 2) + 1];
        //    vertebrae.get(i).setDisplacement(vertebralDisp);

        //    vertebrae.get(i).setSagittalTilt(sagTilt);

        //    heightsDiff = leftDisp - rightDisp;
        //    transTilt = (float)Math.Atan(heightsDiff / distLRSensors);
        //    vertebrae.get(i).setTransverseTilt(transTilt);

        //    vertebrae.get(i).setLeftDisplacement(leftDisp);
        //    vertebrae.get(i).setRightDisplacement(rightDisp);
        //}
    }
    //compute vertebrae loads (for 3d modelling)
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

        //for (int i = 0; i < numBones; i++)
        //{

        //    float vertebralLoad;
        //    float leftLoad, rightLoad;

        //    vertebralLoad = sectional_loads[i];
        //    leftLoad = sensors_loads[i * 2];
        //    rightLoad = sensors_loads[(i * 2) + 1];

        //    vertebrae.get(i).setLoad(vertebralLoad);
        //    vertebrae.get(i).setLeftLoad(leftLoad);
        //    vertebrae.get(i).setRightLoad(rightLoad);
        //}
    }
    //Compute sensor displacement with distance
    private void computeDisplacementWithDistance()
    {
        for (int i = 0; i < sensors_displacements.Length; i++)
        {
            this.sensors_displacements[i] = this.converted_data[i] - this.sensors_loads1[i];
        }
    }
    //Find the most engaged section based on displacement
    private void findSectionEngaged()
    {
        

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
    //Compute force for a single sensor
    private float computeSensorForce(int sensorIndex)
    {
        //Formula needs to be optimised 
        //Different calculations based on stiffness settingd
        if(bluetoothManager.IsStiff){
            
            float sensorForce = (1 - this.converted_data[sensorIndex] / 40) * 146;
            return sensorForce;
        
            
        } else{
            float sensorForce = (1 - this.converted_data[sensorIndex] / 30) * 219;
            
            return sensorForce;
        }
    }
    //Compute force for a section (average of two sensors)
    private float computeSectionForce(int focusSectionIndex)
    {
        float force = (computeSensorForce(focusSectionIndex * 2 + 0) + computeSensorForce(focusSectionIndex * 2 + 1)) / 2 - 5.0f;
        if (force <= 0.0f){
            return 0.0f;
        }else if (force >= 30){
            return 30.0f;
        }else{
            return force; 
        }
            
    }
}

