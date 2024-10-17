using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public Slider delaySlider;
    public Slider boneGapSlider;
    public Slider boneLengthSlider;
    public Slider lowPassfilterSlider;
    public TMP_Text boneLengthSliderText;
    public TMP_Text boneGapSliderText;
    public TMP_Text delaytTimeSliderText;
    public TMP_Text lowPassFilterText;
    public BoneGroupController SagittalBoneControllerScript;
    public BoneGroupController TranverseBoneControllerScript;
    public BluetoothReceiverSuperClass bluetoothDataReceiver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boneLengthSlider.value = BoneControllerScript.boneLength;
        boneGapSlider.value = BoneControllerScript.boneGap;
        delaySlider.value = bluetoothDataReceiver.delayTime;
        lowPassfilterSlider.value = bluetoothDataReceiver.lowPassFilter;


        boneLengthSliderText.text = "BoneLength:" + boneLengthSlider.value.ToString();
        boneGapSliderText.text = "BoneGap:" + boneGapSlider.value.ToString();
        delaytTimeSliderText.text = "DelayTime:" + delaySlider.value.ToString("0.00");
        lowPassFilterText.text = "Filter:" + lowPassfilterSlider.value.ToString("0.0");
    }

    public void changeBoneLength()
    {
        int length = (int) boneLengthSlider.value;
        BoneControllerScript.boneLength = length;
        //TranverseBoneControllerScript.changeBoneLength(length);
        //SagittalBoneControllerScript.changeBoneLength(length);

    }

    public void changeBoneGap()
    {
        int gap = (int)boneGapSlider.value;
        BoneControllerScript.boneGap = gap;
        //TranverseBoneControllerScript.changeBoneGap(gap);
        //SagittalBoneControllerScript.changeBoneGap(gap);
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
