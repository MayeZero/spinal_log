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
        public static bool stiffOn = false;

        public void Start()
        {
            stiffSwitcher.isOn = stiffOn; // update current stiff button 
            updateStiffMenu();
        }




        // Update current state of menu buttons
        public void updateStiffMenu()
        {
            bluetoothManager.setStiff(stiffOn);
            practiceButton.gameObject.SetActive(!stiffOn);
            recordingButton.gameObject.SetActive(!stiffOn);
            stiffButton.gameObject.SetActive(stiffOn);
        }


        // Call when stiff switcher is called 
        public void stiffModeChange()
        {
            stiffOn = stiffSwitcher.isOn;
            updateStiffMenu();
        }
    }
}
