using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using XCharts.Runtime;

public class ButtonsHandllers : MonoBehaviour
{
    public float timeLeft = 10;
    public static bool timerOn = false;
    public TMP_Text timerTxt;


    public static void OnSaveButtonClicked()
    {
        timerOn = true;
        ButtonBlinkEffect.onRecord = true;

    }

    public void OnLoadButtonClicked()
    {
        DataPersistenceManager.instance.LoadGraph();
    }

    public void OnClearButtonClicked()
    {
        DataPersistenceManager.instance.NewGraph();
    }

    public static bool activated()
    {
        return timerOn;
    }


    /// <summary>
    /// Save graph to data
    /// </summary>
    public static void saveGraph()
    {
        DataPersistenceManager.instance.SaveGraph();
        timerOn = false;
        ButtonBlinkEffect.onRecord = false;
    }






    //public static void LoadMainMenu()
    //{
    //    SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    //}

    //void DoDelayAction()
    //{
    //    StartCoroutine(DelayAction());
    //}
    //IEnumerator DelayAction()
    //{
    //    //Wait for the specified delay time before continuing.
    //    while (timeLeft > 0)
    //    {
    //        timerTxt.text = "Seconds left: " + ((int)timeLeft);
    //        timeLeft -= Time.deltaTime; //we where never changing the firetime
    //        yield return null;
    //    }

    //    //Do the action after the delay time has finished.
    //    DataPersistenceManager.instance.SaveGraph();
    //}


}
