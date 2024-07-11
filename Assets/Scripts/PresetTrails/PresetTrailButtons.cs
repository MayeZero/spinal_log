using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresetTrailButtons : MonoBehaviour
{
    //public void LoadShortTrail()
    //{
    //    SceneManager.LoadScene("Assets/Scenes/ShortTrial.unity");
    //    //SceneManager.LoadScene(1);
    //}

    //public void LoadCustomTrail()
    //{
    //    SceneManager.LoadScene("Assets/Scenes/CustomTrail.unity");
    //}

    public void LoadMainMenu() 
    {
        Debug.Log("Main Menu Loaded.");
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }
}
