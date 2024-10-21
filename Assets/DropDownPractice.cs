using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownPractice : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] ShowingGraphPreset graph;
    private List<string> graphNames = new List<string>() { "expertTrial_short.csv", "expertTrial.csv", "expertTrial-600.csv", "empty_csv.csv" };

    // Start is called before the first frame update

    void Start()
    {
        dropdown.options.Clear();
        dropdown.AddOptions(graphNames);
        DropdownSample();

    }


    public void DropdownSample()
    {
        int index = dropdown.value;
        string selectedFile = dropdown.options[index].text;
        graph.resetToFile(selectedFile);
    }


}
