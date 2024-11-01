using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XCharts.Runtime;

public class DropDownScript : MonoBehaviour
{
    [SerializeField] private TMP_Text DataSourceText;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Text SelectText;
    int countIndex = 0;
    //bool selected = false;


    // Start is called before the first frame update

    private void Start()
    {
        dropdown.options.Clear();
        dropdown.options.Add(new TMP_Dropdown.OptionData("New"));
        addNewOption(1);  // default file G1
        countIndex = 1;

        // add additional available files
        int fileCount = DataPersistenceManager.instance.getFileCount();
        Debug.Log("Total recorded files: " + fileCount);
        for (int i = 2; i < fileCount + 1; i++)
        {
            addNewOption(i);
        }

        countIndex = fileCount;
    }

    private void Awake()
    {
    }


    public void DropdownSample()
    {
        SelectText.gameObject.SetActive(false);
        int index = dropdown.value;

        if (index == 0)
        {
            countIndex += 1;
            index = countIndex;
            addNewOption(index);
            DataPersistenceManager.instance.fileCount += 1;
            dropdown.value = index;
        }

        string filename = getFileName(index);
        DataPersistenceManager.instance.setFileName(filename);
        DataSourceText.text = DataPersistenceManager.instance.getFileName();
    }

    private string getFileName(int index)
    {
        return "data" + (index) + ".csv";
    }


    private void addNewOption(int option)
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData("G" + option));
        dropdown.RefreshShownValue();
    }
}
