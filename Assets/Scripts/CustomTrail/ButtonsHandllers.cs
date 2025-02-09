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
    [SerializeField] GameObject recordButton;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject clearButton;

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }


    public void OnSaveButtonClicked()
    {
        DataPersistenceManager.instance.NewGraph(); // open new blank graph
        graph.cleanGraph();
        DeactivateRecordButton(); // hide record button, show pause button
        recordingPanel.startRecord(); // blinking effect
 

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
    /// Save graph to data at stop recording
    /// </summary>
    public void saveGraph()
    {
        DataPersistenceManager.instance.SaveGraph(); // save all current data points to the graph
        recordingPanel.stopRecord(); // stop blinking effects
        ActivateRecordButton(); // re-activate record button
        LoadJustMadeGraphAsTemplate(); // load newly made template
    }

    private void LoadJustMadeGraphAsTemplate()
    {
        OnLoadButtonClicked();
        graph.addCustomTrailToGraph();   // add just-made graph as template
        graph.cleanRealTimeData();  // reset starting point for real-time data
    }

    
    public void ActivateRecordButton()
    {
        recordButton.SetActive(true);
        pauseButton.SetActive(false);
        clearButton.SetActive(true);
    }

    public void DeactivateRecordButton()
    {
        recordButton.SetActive(false);
        pauseButton.SetActive(true);
        clearButton.SetActive(false);
    }









}
