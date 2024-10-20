using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StiffDropDown : MonoBehaviour
{

    [SerializeField] Button practiceButton;
    [SerializeField] Button recordingButton;
    [SerializeField] Button stiffButton;     // 0: default, 1: stiff
    [SerializeField] TMP_Dropdown stiffSwitcher;
    [SerializeField] BluetoothManager bluetoothManager;
    private static bool isStiff = false;

    private void Start()
    {
        stiffSwitcher.value = isStiff ? 1 : 0;
        updateStiffMenu();
    }

    public void stiffModeChange()
    {
        bool value = stiffSwitcher.value == 1;   // true if dropdown value is stiff
        isStiff = value;
        updateStiffMenu();
    }


    public void updateStiffMenu()
    {
        bluetoothManager.setStiff(isStiff);
        practiceButton.gameObject.SetActive(!isStiff);
        recordingButton.gameObject.SetActive(!isStiff);
        stiffButton.gameObject.SetActive(isStiff);
    }




}
