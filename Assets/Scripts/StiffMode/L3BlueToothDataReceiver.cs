using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;

public class L3BlueToothDataReceiver : BluetoothReceiverSuperClass
{
    [SerializeField] ShowingGraphPreset graph;
    [SerializeField] Text output;
    //[SerializeField] Text log;
    public string sensorDatainString;

    //public float[] converted_data = new float[3];
    //public float[] sensors_loads1 = new float[3];

    public string input;

    private IEnumerator myCoroutine2;
    public float focusSectionForce;
    private float initialAvgForce;
    public float scaleLevel = 2.0f;

    private void Awake()
    {
        scaleLevel = 2.0f;
    }
    void Start()
    {
        // Initialize variables and start data processing coroutine
        bluetoothManager = FindObjectOfType<BluetoothManager>();
        connected = true;
        focusSectionForce = currentData = targetData = 0.0f;
        if (myCoroutine2 != null)
        {
            StopCoroutine(myCoroutine2);
        }
        myCoroutine2 = DataProcessing(delayTime);
        Debug.Log("my small coroutine set");
        StartCoroutine(myCoroutine2);
        Debug.Log("Coroutine minispinal started");
    }

    // Coroutine for continuous data processing 
    private IEnumerator DataProcessing(float waitTime)
    {
        while (true)
        {
            sensorDatainString = bluetoothManager.inputdata;
            converted_data = ConvertedFloat(sensorDatainString);

            focusSectionForce = converted_data[0] * scaleLevel;

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

            output.text = "Force: " + focusSectionForce;

            yield return new WaitForSeconds(waitTime);
        }

    }
    //Convert string input to float array
    private float[] ConvertedFloat(string inputData)
    {
        float[] convertData = new float[3];
        convertData = Array.ConvertAll(inputData.Split(','), float.Parse);
        return convertData;
    }


    public void sendData1(){
        bluetoothManager.SendData("1");
    }

    public async void sendData2(){
        bluetoothManager.SendData("1");
        await Task.Delay(2000);
        bluetoothManager.SendData("2");
    }
    public async void sendData3(){
        bluetoothManager.SendData("1");
        await Task.Delay(2000);
        bluetoothManager.SendData("3");
    }

    
 
}
