using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISwitcher { 
    public class StiffControl : MonoBehaviour
    {

        [SerializeField] Button practiceButton;
        [SerializeField] Button recordingButton;
        [SerializeField] Button stiffButton;
        [SerializeField] UISwitcher stiffSwitcher;
        [SerializeField] BluetoothManager bluetoothManager;


        public void stiffModeChange()
        {
            bool value = stiffSwitcher.isOn;
            bluetoothManager.setStiff(value);
            practiceButton.gameObject.SetActive(!value);
            recordingButton.gameObject.SetActive(!value);
            stiffButton.gameObject.SetActive(value);
        }
    }
}
