using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropDownScript : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    // Start is called before the first frame update
 
    public void DropdownSample(int index)
    {
        switch (index)
        {
            case 0:
                DataPersistenceManager.instance.setFileName("data.csv");
                numberText.text = DataPersistenceManager.instance.getFileName();
                break;
            case 1: 
                DataPersistenceManager.instance.setFileName("data1.csv");
                numberText.text = DataPersistenceManager.instance.getFileName();
                break;
            case 2: 
                DataPersistenceManager.instance.setFileName("data2.csv");
                numberText.text = DataPersistenceManager.instance.getFileName();
                break;

        }
    }
}
