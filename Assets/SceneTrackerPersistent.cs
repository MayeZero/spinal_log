using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

public class SceneTrackerPersistent : MonoBehaviour
{
    // Static variable to track the number of times the scene is loaded
    private static int sceneLoadCount = 0;

    //int sceneLoadCount = PlayerPrefs.GetInt("SceneLoadCount", 0); //Option 2: reset for uninstallation


    void Awake()
    {
        // Increase the count each time the scene is loaded
        sceneLoadCount++;

        if (sceneLoadCount == 1)
        {
            Debug.Log("Scene loaded times: 1");
            DataPersistenceManager.instance.deleteAllFiles();
        }
        else
        {
            Debug.Log("Scene loaded times: " + sceneLoadCount.ToString());
            DataPersistenceManager.instance.printAllFiles();
        }
    }
}
