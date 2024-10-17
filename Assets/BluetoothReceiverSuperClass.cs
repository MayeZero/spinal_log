using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BluetoothReceiverSuperClass : MonoBehaviour
{
    // Start is called before the first frame update
    protected BluetoothManager bluetoothManager;
    public bool connected = false;
    public float[] converted_data = new float[8];
    public float delayTime = 0.08f;

    // This field controls the low pass value
    // Use 1 for no filtering, and a value closer to zero for more sluggish filtering 
    // (Note that zero would be invalid and freeze the transform)
    //
    [Range(0.1f, 1.0f)]
    public float lowPassFilter = 0.5f;
    public float LowPassFilter { set { lowPassFilter = value; } }
    public float currentData, targetData;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
