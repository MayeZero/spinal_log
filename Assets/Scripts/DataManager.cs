using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<double> randomData { get; private set; } = new List<double>();

    private void Start()
    {
        randomData = new List<double>();
    }

    public void GenerateRandomData()
    {
        if (randomData.Count > 300)
        {
            ClearData();
        }
        //randomData = new List<double>();
        
        //for (int i = 0; i < 2; i++)
        //{
        //    double randomValue = Random.Range(0.0f, 25.0f);
        //    randomData.Add(randomValue);
        //}
        double randomValue = Random.Range(0.0f, 25.0f);
        randomData.Add(randomValue);

        Debug.Log("Random data generated:" + randomValue);
        Debug.Log("Data number: " + randomData.Count);
        //foreach (double value in randomData)
        //{
        //    Debug.Log(value);
        //}
    }

    public void ClearData()
    {
        randomData.Clear();
    }
}
