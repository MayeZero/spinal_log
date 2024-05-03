using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void LoadShortTrail()
    {
        SceneManager.LoadScene("Assets/Scenes/ShortTrial.unity");
        //SceneManager.LoadScene(1);
    }

    public void LoadCustomTrail()
    {
        SceneManager.LoadScene("Assets/Scenes/CustomTrial.unity");
    }
}
