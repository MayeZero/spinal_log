using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class csvReader : MonoBehaviour
{
    //public TextAsset expertTrail;

    // Start is called before the first frame update
    void Start()
    {


        List<double> data = srdCSVFile();
        foreach (double value in data)
        {
            Debug.Log(value);
        }
    }


    

    public static List<double> srdCSVFile()
    {
        List<double> records = new List<double>();

        string sFilePath = Path.Combine(Application.streamingAssetsPath, "expertTrial_short.csv");
        
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(sFilePath);
            www.SendWebRequest();
            while (!www.isDone) ;
            try
            {
                string filePath = Application.streamingAssetsPath + "/expertTrial_short.csv";
                //Debug.Log(Application.persistentDataPath);
                //using (StreamReader reader = new StreamReader("Assets/Trials/expertTrial_short.csv"))
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    reader.ReadLine(); // Skip the header line

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            double value = double.Parse(line);
                            records.Add(value);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error reading file: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Debug.LogError($"Error parsing CSV data: {ex.Message}");
            };
        }
        else 


        try
        {
            string filePath = Application.streamingAssetsPath + "/expertTrial_short.csv";
            //Debug.Log(Application.persistentDataPath);
            //using (StreamReader reader = new StreamReader("Assets/Trials/expertTrial_short.csv"))
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                reader.ReadLine(); // Skip the header line

                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        double value = double.Parse(line);
                        records.Add(value);
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error reading file: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Debug.LogError($"Error parsing CSV data: {ex.Message}");
        }

        return records;
    }


}
