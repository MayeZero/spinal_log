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
    public Slider graphScaler;

    public TMP_Text boneLengthSliderText;
    public TMP_Text boneGapSliderText;
    public TMP_Text delaytTimeSliderText;
    public TMP_Text lowPassFilterText;
    public TMP_Text graphScalerText;

    public BoneGroupController SagittalBoneControllerScript;
    public BoneGroupController TranverseBoneControllerScript;
    public BluetoothReceiverSuperClass bluetoothDataReceiver;
    // Start is called before the first frame update
    void Start()
    {
        updateSlideInfo();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeBoneLength()
    {
        int length = (int) boneLengthSlider.value;
        BoneControllerScript.boneLength = length;
        updateSlideInfo();
        //TranverseBoneControllerScript.changeBoneLength(length);
        //SagittalBoneControllerScript.changeBoneLength(length);

    }

    public void changeBoneGap()
    {
        int gap = (int)boneGapSlider.value;
        BoneControllerScript.boneGap = gap;
        updateSlideInfo();
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
        updateSlideInfo();
    }

    public void changeFilterValue()
    {
        float filterValue = lowPassfilterSlider.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.lowPassFilter = filterValue;
        }
        updateSlideInfo();
    }

    public void changeGraphScaler()
    {
        float graphScale = graphScaler.value;
        if (bluetoothDataReceiver != null && bluetoothDataReceiver.connected)
        {
            bluetoothDataReceiver.graphScale = graphScale;
        }
        updateSlideInfo();
    }


    public void updateSlideInfo()
    {
        boneLengthSlider.value = BoneControllerScript.boneLength;
        boneGapSlider.value = BoneControllerScript.boneGap;
        delaySlider.value = bluetoothDataReceiver.delayTime;
        lowPassfilterSlider.value = bluetoothDataReceiver.lowPassFilter;
        graphScaler.value = bluetoothDataReceiver.graphScale;


        boneLengthSliderText.text = "Tranverse Insensitivity: " + boneLengthSlider.value.ToString();
        boneGapSliderText.text = "Sagittal Insensitivity: " + boneGapSlider.value.ToString();
        delaytTimeSliderText.text = "DataReceived DelayTime: " + delaySlider.value.ToString("0.00");
        lowPassFilterText.text = "LowPass Filter Value:" + lowPassfilterSlider.value.ToString("0.0");
        graphScalerText.text = "Graph Scaler Value: " + graphScaler.value.ToString("0.0");
    }
}
