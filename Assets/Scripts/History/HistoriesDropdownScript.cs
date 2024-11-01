using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XCharts.Runtime;
using System.IO;

public class HistoriesDropdownScript : MonoBehaviour
{
    [SerializeField] private TMP_Text DataSourceText;
    public TMP_Dropdown folderDropdown;
    public TMP_Dropdown fileDropDown;
    //public TMP_Dropdown historyDropDown;
    private List<string> graphNames = new List<string>() { "expertTrial.csv", "expertTrial-600.csv", "expertTrial_short.csv", "empty_csv.csv" };
    private List<string> folderNames;
    [SerializeField] HistoryGraph historyGraph;
    int countIndex = 0;


    // Start is called before the first frame update

    private void Start()
    {
        folderNames = new List<string>() { Application.streamingAssetsPath, Application.persistentDataPath };
        setFolderDropdown();
        setFileDropDown();
    }

    private void setFolderDropdown()
    {
        folderDropdown.options.Clear();
        folderDropdown.options.Add(new TMP_Dropdown.OptionData("Patterns"));
        folderDropdown.options.Add(new TMP_Dropdown.OptionData("User histories"));
    }

    public void setFileDropDown()
    {
        fileDropDown.options.Clear();

        if (folderDropdown.value == 1)
        {
            fileDropDown.options.Add(new TMP_Dropdown.OptionData("History"));

            // add additional available files
            int fileCount = DataPersistenceManager.instance.getFileCount();
            Debug.Log("Total recorded files: " + fileCount);
            for (int i = 1; i < fileCount + 1; i++)
            {
                addNewOption(i);
            }

            countIndex = fileCount;
        } else
        {
            fileDropDown.AddOptions(graphNames);
        }

        fileDropDown.value = 0;
        //fileDropDown.RefreshShownValue();
    }

    private void Awake()
    {
    }


    public void DropdownSample()
    {
        int index = fileDropDown.value;
        Debug.Log("Folder name: " + folderNames);
        string folderName = folderNames[folderDropdown.value];

        // skip if we are at history 0
        if (folderDropdown.value == 1 && index == 0)
        {
            DataSourceText.text = string.Empty;
            return;
        }

        
        string filename = fileDropDown.options[index].text;
        DataSourceText.text = filename;

        if (folderDropdown.value == 0)
        {
            historyGraph.setPattern(filename);
        }
        else
        {
            historyGraph.setData(folderName, filename);
            //historyGraph.addCustomTrailToGraph();
        }

    }

    public string getFileName(int index)
    {
        return "data" + (index) + ".csv";
    }


    private void addNewOption(int option)
    {
        fileDropDown.options.Add(new TMP_Dropdown.OptionData(getFileName(option)));
    }


    public void deleteSelectedFile()
    {
        int folderValue = folderDropdown.value;
        int value = fileDropDown.value;
        string foldername = folderNames[folderValue];

        
        // rename all files if we are at history folder
        if (folderValue == 1 && DataPersistenceManager.instance.fileCount > 0)
        {
            DataPersistenceManager.instance.fileCount -= 1;
            FileHandler.deleteFile(foldername, fileDropDown.options[value].text);
            FileHandler.renameAllFiles(Application.persistentDataPath);
        }

        setFileDropDown();
    }


    public void deleteFile(string dir, string file)
    {
        FileHandler.deleteFile(dir, file);
    }
}
