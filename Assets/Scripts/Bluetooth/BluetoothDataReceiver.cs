using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BluetoothDataReceiver : MonoBehaviour
{
   
    private BluetoothManager bluetoothManager;
    public Text output;
    public Text log;
    public string data;
    

    void Start()
    {
        bluetoothManager = FindObjectOfType<BluetoothManager>();
        output.text = bluetoothManager.inputdata;
        if (bluetoothManager != null)
        {
            bluetoothManager.BluetoothDataReceived += HandleBluetoothData;
        }
        else
        {
            log.text = "Bluttooth Manager not found!";
        }
    }

    private void Update()
    {

        //output.text = bluetoothManager.inputdata;
        output.text = bluetoothManager.inputdata;
        data = bluetoothManager.inputdata;
        log.text = data;
    }

    private void HandleBluetoothData(object sender, BluetoothDataEventArgs e)
    {
        // Process the received Bluetooth data
        output.text = e.Data;
        data = e.Data;

        Debug.Log("Received Bluetooth data: " + e.Data);
    }

    private void OnDestroy()
    {
        if (bluetoothManager != null)
        {
            bluetoothManager.BluetoothDataReceived -= HandleBluetoothData;
        }
    }

    static string GenerateRandomNumbers()
    {
        
        int[] numbers = new int[8];

        // Generate 8 random numbers between 0 and 30
        for (int i = 0; i < 8; i++)
        {
            numbers[i] = Random.Range(0, 31); // 0 inclusive, 31 exclusive
        }

        // Join the numbers into a string separated by commas
        string result = string.Join(",", numbers);

        return result;
    }
}
