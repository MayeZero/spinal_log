using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject connectButton;
    [SerializeField] GameObject disconnectButton;

    private void Start()
    {
        UpdateConnectionButtons();
    }

    public void UpdateConnectionButtons()
    {
        BluetoothManager bluetoothManager = FindObjectOfType<BluetoothManager>();
        if (bluetoothManager != null && bluetoothManager.IsConnected()) 
        { 
            connectButton.SetActive(false);
            disconnectButton.SetActive(true);
        } else
        {
            connectButton.SetActive(true);
            disconnectButton.SetActive(false);
        }

    }

    public void LoadShortTrail()
    {
        SceneManager.LoadScene("Assets/Scenes/PracticeMode.unity");
        //SceneManager.LoadScene(1);
    }

    public void LoadCustomTrail()
    {
        SceneManager.LoadScene("Assets/Scenes/CustomTrial.unity");
    }

    public void LoadBTConnection()
    {
        SceneManager.LoadScene("Assets/Scenes/BTConnection.unity");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }

    public void Load3DModel()
    {
        SceneManager.LoadScene("Assets/Scenes/3DModel.unity");
    }

    public void LoadFileMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/Manage.unity");
    }

    public void LoadStiffMode()
    {
        SceneManager.LoadScene("Assets/Scenes/StiffMode.unity");
    }


    


}
