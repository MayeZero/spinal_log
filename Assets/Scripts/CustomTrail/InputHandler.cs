using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] InputField inputField;
    [SerializeField] DropDownScript dropDownScript;
    [SerializeField] ButtonsHandllers buttonsHandllers;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] GameObject InputBox;

     
    public void ValidateInput()
    {
        string input = inputField.text;

        if (input.Length <= 0)
        {
            // invalid input
            descriptionText.text = "Invalid input";
        } else if (DataPersistenceManager.instance.CheckFileExistInHashSet(input))
        {
            // duplicate input
            descriptionText.text = "File name existed!";
        } else // start recording
        {
            dropDownScript.addNewDataFile(input); // add new data file to dropdown 
            buttonsHandllers.OnSaveButtonClicked(); // recording functions
            InputBox.SetActive(false); // remove the box
        }
    }

    public void ResetInputBox()
    {
        InputBox.SetActive(true);
        descriptionText.text = "Enter file name here";
        inputField.text = string.Empty;
    }

    

}
