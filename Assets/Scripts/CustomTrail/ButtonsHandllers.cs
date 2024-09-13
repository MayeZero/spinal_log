using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsHandllers : MonoBehaviour
{
    public float timeLeft = 24f;
    public bool timerOn = false;
    public TMP_Text timerTxt;

    // Start is called before the first frame update

    //void Update()
    //{
    //    if (timerOn)
    //    {
    //        if (timeLeft > 0)
    //        {
    //            timeLeft -= Time.deltaTime;
    //            timerTxt.text = "Seconds left: " + timeLeft.ToString();
    //        } else
    //        {
    //            DataPersistenceManager.instance.SaveGraph();
    //            timerTxt.text = "Data saved to:" + DataPersistenceManager.instance.getFileName();
    //            timeLeft = 0;
    //            timerOn = false;
    //        }
    //    }
    //}


    public void OnSaveButtonClicked()
    {
        timerOn = true;
        timeLeft = 24f;
        DataPersistenceManager.instance.SaveGraph();

    }

    public void OnLoadButtonClicked()
    {
        DataPersistenceManager.instance.LoadGraph();
    }

    public void OnClearButtonClicked()
    {
        DataPersistenceManager.instance.NewGraph();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }

    //private void savingGraph()
    //{
    //    DataPersistenceManager.instance.SaveGraph();
    //}
}
