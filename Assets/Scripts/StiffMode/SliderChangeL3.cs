using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderChangeL3 : MonoBehaviour
{
    public Slider delaySlider;
    public Slider lowPassfilterSlider;

    public TMP_Text delaytTimeSliderText;
    public TMP_Text lowPassFilterText;

    public BluetoothReceiverSuperClass bluetoothDataReceiver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        delaySlider.value = bluetoothDataReceiver.delayTime;
        lowPassfilterSlider.value = bluetoothDataReceiver.lowPassFilter;

        delaytTimeSliderText.text = "DelayTime:" + delaySlider.value.ToString("0.00");
        lowPassFilterText.text = "Filter:" + lowPassfilterSlider.value.ToString("0.0");
    }

    



    public void changeDelayTime()
    {
        float delayTime = delaySlider.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.delayTime = delayTime;
        }
    }

    public void changeFilterValue()
    {
        float filterValue = lowPassfilterSlider.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.lowPassFilter = filterValue;
        }
    }
}
