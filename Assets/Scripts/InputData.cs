using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputData : MonoBehaviour
{
    public Button generateButton;
    //public List<double> randomData;
    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = GetComponent<DataManager>();
        //randomData = new List<double>();
        generateButton.onClick.AddListener(GenerateAndUpdateData);
    }


    //private void GenerateRandomData()
    //{

    //    for (int i = 0; i < 5; i++)
    //    {
    //        double randomValue = Random.Range(0.0f, 50.0f);
    //        randomData.Add(randomValue);
    //    }
        
    //    Debug.Log("Random data generated:");
    //    Debug.Log("Data number: " + randomData.Count);
    //    foreach (double value in randomData)
    //    {
    //        Debug.Log(value);
    //    }
        
    //}

    private void GenerateAndUpdateData()
    {
        dataManager.GenerateRandomData();
    }
}
