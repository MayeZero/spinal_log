using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using XCharts.Runtime;

public class ButtonsHandllers_History: MonoBehaviour
{
    public float countdown = 3f;
    //[SerializeField] RecordingPanelScript recordingPanel;
    [SerializeField] HistoryGraph graph;
    //[SerializeField] TMP_Dropdown historyDropdown;
    [SerializeField] HistoriesDropdownScript historyDropdownScript;


    private void Start()
    {
        historyDropdownScript = this.gameObject.GetComponent<HistoriesDropdownScript>();
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }


    public void OnDeleteButtonClicked()
    {
        //int value = historyDropdown.value;
        //FileHandler.deleteFile(Application.persistentDataPath, historyDropdown.GetComponent<HistoriesDropdownScript>().getFileName(value));
        //FileHandler.renameAllFiles(Application.persistentDataPath);
        //historyDropdown.GetComponent<HistoriesDropdownScript>().setDropDown();
        historyDropdownScript.deleteSelectedFile();

    }




    //public bool activated()
    //{
    //    return RecordingPanelScript.onRecord;
    //}


    /// <summary>
    /// Save graph to data
    /// </summary>
    //public void saveGraph()
    //{
    //    DataPersistenceManager.instance.SaveGraph();
    //    recordingPanel.stopRecord();
    //}







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
