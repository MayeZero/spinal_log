using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderChangeL3 : MonoBehaviour
{
    public Slider delaySlider;
    public Slider lowPassfilterSlider;
    public Slider scaleLevelSlider;
    public Slider boneSlider;

    public TMP_Text delaytTimeSliderText;
    public TMP_Text lowPassFilterText;
    public TMP_Text scaleLevelSliderText;
    public TMP_Text boneSliderText;

    public L3BlueToothDataReceiver bluetoothDataReceiver;
    // Start is called before the first frame update
    void Start()
    {
        updateSliderInfo();
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    void updateSliderInfo()
    {
        delaySlider.value = bluetoothDataReceiver.delayTime;
        lowPassfilterSlider.value = bluetoothDataReceiver.lowPassFilter;
        scaleLevelSlider.value = bluetoothDataReceiver.scaleLevel;
        boneSlider.value = L3Controller.boneMoveValue;

        scaleLevelSliderText.text = "Force Graph Scale Value: " + scaleLevelSlider.value.ToString("0.0");
        delaytTimeSliderText.text = "Data Received Delay Time: " + delaySlider.value.ToString("0.00");
        lowPassFilterText.text = "Low Pass Filter: " + lowPassfilterSlider.value.ToString("0.0");
        boneSliderText.text = "Bone Move Scale Value: " + boneSlider.value.ToString("0.0");
    }


    public void changeDelayTime()
    {
        float delayTime = delaySlider.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.delayTime = delayTime;
        }

        // Update slider values 
        updateSliderInfo();
    }

    public void changeFilterValue()
    {
        float filterValue = lowPassfilterSlider.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.lowPassFilter = filterValue;
        }
        // Update slider values 
        updateSliderInfo();
    }


    public void changeScaleValue()
    {
        float scaleValue = scaleLevelSlider.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.scaleLevel = scaleValue;
        }
        // Update slider values 
        updateSliderInfo();
    }


    public void changeBoneMoveValue()
    {
        float boneMoveValue = boneSlider.value;
        L3Controller.boneMoveValue = boneMoveValue;
        // Update slider values 
        updateSliderInfo();
    }
}
