using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SLDataManager : MonoBehaviour
{
    public List<double> inputData { get; private set; } = new List<double>();
    public List<double> dataFromCustomTrail { get; private set; } = new List<double>();
    private const int MAX_DATA_COUNT = 300;
    private const string DATA_FILE_NAME = "customTrail.csv";

    void Start()
    {
        inputData = new List<double>();
    }

    void Update()
    {

    }

    public void InputForceData()
    {
        if (inputData.Count >= MAX_DATA_COUNT)
        {
            //SaveDataToCSV(inputData, DATA_FILE_NAME, Application.persistentDataPath);
            ClearData();
        }

        //using random data at this stage.
        double randomValue = Random.Range(0.0f, 25.0f);
        inputData.Add(randomValue);
        Debug.Log("Random data generated: " + randomValue);
        Debug.Log("Data number: " + inputData.Count);
    }

    public void ClearData()
    {
        inputData.Clear();
    }

    public void SaveDataToCSV(List<double> data, string filename, string path)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Force");

        foreach (double value in data)
        {
            stringBuilder.AppendLine(value.ToString());
        }

        string filePath = Path.Combine(path, filename);
        File.WriteAllText(filePath, stringBuilder.ToString());
        Debug.Log($"Data saved to {filePath}");
    }

    public void ReadDataFromCustomTrail(string fileName, string path)
    {
        string filePath = Path.Combine(path, fileName);
        
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found at {filePath}");
            return;
        }
        dataFromCustomTrail.Clear();

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            if (double.TryParse(lines[i], out double value))
            {
                dataFromCustomTrail.Add(value);
            }
            else
            {
                Debug.LogWarning($"Skipping invalid data at line {i}: {lines[i]}");
            }

            Debug.Log($"Read {dataFromCustomTrail.Count} data points from {filePath}");
        }
    }

}
