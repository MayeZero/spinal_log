using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using XCharts.Runtime;

public class ButtonsHandllers : MonoBehaviour
{
    public float countdown = 3f;
    [SerializeField] RecordingPanelScript recordingPanel;
    [SerializeField] ShowCustomGraphYT graph;

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }


    public void OnSaveButtonClicked()
    {
        DataPersistenceManager.instance.NewGraph();
        graph.cleanGraph();
        recordingPanel.startRecord();
 

    }

    public void OnLoadButtonClicked()
    {
        DataPersistenceManager.instance.LoadGraph();
    }

    public void OnClearButtonClicked()
    {
        DataPersistenceManager.instance.NewGraph();
    }

    public bool activated()
    {
        return RecordingPanelScript.onRecord;
    }


    /// <summary>
    /// Save graph to data
    /// </summary>
    public void saveGraph()
    {
        DataPersistenceManager.instance.SaveGraph();
        recordingPanel.stopRecord();
    }







    //IEnumerator DoTimer(float counttime = 1f)
    //{
    //    int count = 0;
    //    while (timerOn)
    //    {
    //        yield return new WaitForSeconds(counttime);
    //        count ++;
    //        TimeSpan time = TimeSpan.FromSeconds(count);
    //        string formattedTime = time.ToString(@"mm\:ss");
    //        timerTxt.text = formattedTime;
    //    }
    //}

    //void newTimer()
    //{
    //    StartCoroutine(DoTimer());
    //}



}
