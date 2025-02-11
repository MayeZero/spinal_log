using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XCharts.Runtime;
using System.IO;

public class DropDownScript : MonoBehaviour
{
    [SerializeField] private TMP_Text DataSourceText;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Text SelectText;
    HashSet<string> fileNames = new HashSet<string>();
    int countFile = 0;
    //bool selected = false;


    // Start is called before the first frame update

    private void Start()
    {
        dropdown.options.Clear();
        int fileCount = DataPersistenceManager.instance.getFileCount();
        Debug.Log("Total recorded files: " + fileCount);

        // Set up the dropdown list (with available files)
        foreach (string fileName in DataPersistenceManager.instance.getAllFiles())
        {
            addNewOptionToDropDown(Path.GetFileNameWithoutExtension(fileName));
            countFile++;
        }
    }

    private void Awake()
    {
    }


    /// <summary>
    /// Activate when a dropdown item is selected
    /// </summary>
    public void DropdownSample()
    {
        SelectText.gameObject.SetActive(false);

        // get the current selected filename from dropdown
        int index = dropdown.value;    
        string filename = dropdown.options[index].text;  

        // update the system file using selected filename
        updateSystemFile(filename);
        DataSourceText.text = DataPersistenceManager.instance.getFileName();
    }


    /// <summary>
    /// Update the name of currently used system file
    /// </summary>
    /// <param name="name"></param>
    private void updateSystemFile(string name)
    {
        if (name.Length > 0)
        {
            string fileName = name + ".csv"; // retrieve file name with extension
            DataPersistenceManager.instance.setFileName(fileName);
        }
        
    }




    /// <summary>
    /// Add a new option to dropdown UI
    /// </summary>
    /// <param name="option"></param>
    private void addNewOptionToDropDown(string option)
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData(option));
        dropdown.RefreshShownValue();
    }

    /// <summary>
    /// Add new file and update dropdown UI
    /// </summary>
    public void addNewDataFile(string filename)
    {
        countFile ++;
        addNewOptionToDropDown(filename);
        DataPersistenceManager.instance.fileCount++; // update file count in Data Persistent manager 
        dropdown.value = countFile;  // go to the newest item in the list
        updateSystemFile(filename);
        DataPersistenceManager.instance.AddFileToHashSet(filename);
    }
}
