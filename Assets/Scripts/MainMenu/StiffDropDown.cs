using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StiffDropDown : MonoBehaviour
{

    [SerializeField] Button practiceButton;
    [SerializeField] Button recordingButton;
    [SerializeField] Button stiffButton;
    [SerializeField] TMP_Dropdown stiffSwitcher;
    [SerializeField] BluetoothManager bluetoothManager;


    public void stiffModeChange()
    {
        int value = stiffSwitcher.value;
        switch (value)
        {
            case 1:
                bluetoothManager.setStiff(true);
                practiceButton.gameObject.SetActive(false);
                recordingButton.gameObject.SetActive(false);
                stiffButton.gameObject.SetActive(true);
                break;
            case 0:
                bluetoothManager.setStiff(false);
                practiceButton.gameObject.SetActive(true);
                recordingButton.gameObject.SetActive(true);
                stiffButton.gameObject.SetActive(false);
                break;
        }
      }
}
