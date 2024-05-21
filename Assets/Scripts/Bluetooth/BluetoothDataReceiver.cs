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
    
    [SerializeField] Text output;
    [SerializeField] Text log;

    public string sensorDatainString;

    public float[] converted_data = new float[8];
    public float[] sensors_loads1 = new float[8];
    public float[] sensors_loads2 = new float[8];
    public float[] sensors_displacements = new float[8];
    public int numSensors;
    public int numSections;
    public float[] sectional_displacements = new float[4];
    public float[] sectional_loads = new float[4];
    private int focusSectionIndex;
    public string input;
    public float focusSectionForce;

    private IEnumerator myCoroutine;

    void Start()
    {
        bluetoothManager = FindObjectOfType<BluetoothManager>();
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
        }

        focusSectionIndex = 0;

        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        myCoroutine = DataProcessing(0.01f);
        StartCoroutine(myCoroutine);

        //if (bluetoothManager != null)
        //{
        //    bluetoothManager.BluetoothDataReceived += HandleBluetoothData;
        //}
        //else
        //{
        //    log.text = "Bluttooth Manager not found!";
        //}
    }

    private IEnumerator DataProcessing(float waitTime)
    {
        while (true)
        {
        //output.text = bluetoothManager.inputdata;

        sensorDatainString = bluetoothManager.inputdata;
        converted_data = ConvertedFloat(sensorDatainString);

        computeDisplacementWithDistance();
        findSectionEngaged();
        computeSectionForce();
        graphYT.addRealTimeDataToGraph(focusSectionForce) ;

        string data = focusSectionForce.ToString();//testing 
        
        output.text = "Focused Section: " + focusSectionIndex;
        log.text = "Force: " + data;

        sensors_loads1 = converted_data;

        yield return new WaitForSeconds(waitTime);
        }
        
    }

    private void Update()
    {
        // output.text = bluetoothManager.inputdata;

        // sensorDatainString = bluetoothManager.inputdata;
        // converted_data = ConvertedFloat(sensorDatainString);
        
        // computeDisplacementWithDistance();
        // findSectionEngaged();
        // computeSectionForce();
        // graphYT.addRealTimeDataToGraph(focusSectionForce) ;

        // string data = focusSectionForce.ToString();//testing 

        // log.text = "focused section: " + data;

        // sensors_loads1 = converted_data;
        
    }

    // private void HandleBluetoothData(object sender, BluetoothDataEventArgs e)
    // {
    //     // Process the received Bluetooth data
    //     output.text = e.Data;
    //     sensorDatainString = e.Data;

    //     Debug.Log("Received Bluetooth data: " + e.Data);
    // }

    // private void OnDestroy()
    // {
    //     if (bluetoothManager != null)
    //     {
    //         bluetoothManager.BluetoothDataReceived -= HandleBluetoothData;
    //     }
    // }


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

    private void computeDisplacementWithDistance()
    {
        for (int i = 0; i < sensors_displacements.Length; i++)
        {
            this.sensors_displacements[i] = this.converted_data[i] - this.sensors_loads1[i];
        }
    }
    private void findSectionEngaged()
    {
        this.focusSectionIndex = 3;

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

    private float computeSensorForce(int sensorIndex)
    {
        float sensorForce = (1 - this.converted_data[sensorIndex] / 30) * 219;
        return sensorForce;
    }

    private void computeSectionForce()
    {
        this.focusSectionForce = (computeSensorForce(focusSectionIndex * 2 + 0) + computeSensorForce(focusSectionIndex * 2 + 1)) / 2;
    }
}
