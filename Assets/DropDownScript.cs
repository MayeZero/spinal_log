using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using XCharts.Runtime;

public class DropDownScript : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private TMP_Dropdown dropdown;
    int countIndex = 0;


    // Start is called before the first frame update

    private void Start()
    {
        dropdown.options.Clear();
        dropdown.options.Add(new TMP_Dropdown.OptionData("Load"));
        addNewOption(1);
        countIndex = 1;
    }


    public void DropdownSample()
    {
        int index = dropdown.value;

        if (index == 0)
        {
            countIndex += 1;
            index = countIndex;
            addNewOption(index);
            dropdown.value = index;
        }

        string filename = getFileName(index);
        DataPersistenceManager.instance.setFileName(filename);
        numberText.text = DataPersistenceManager.instance.getFileName();
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
