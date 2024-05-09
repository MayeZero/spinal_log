using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class csvReader : MonoBehaviour
{
    //public TextAsset expertTrail;

    // Start is called before the first frame update
    void Start()
    {

        //List<string> data = srdCSVFile();
        //foreach (string force in data)
        //{
        //    Debug.Log(force);
        //}

        List<double> data = srdCSVFile();
        foreach (double value in data)
        {
            Debug.Log(value);
        }
    }


    //using TextAsset
    

    //using StreamReader
    //public static List<string> srdCSVFile()
    //{
    //    List<string> records = new List<string>();

    //    try
    //    {
    //        using (StreamReader reader = new StreamReader("Assets/Trials/expertTrial_short.csv"))
    //        {
    //            string line;
    //            while ((line = reader.ReadLine()) != null)
    //            {
    //                //Debug.Log(line);
    //                if (line == "Force")
    //                {
    //                    continue;
    //                } else
    //                {
    //                    records.Add(line);
    //                }
                    
    //            }
    //        }
    //    }
    //    catch (IOException ex)
    //    {
    //        Debug.LogError($"Error reading file: {ex.Message}");
    //    }

    //    return records;
    //}

    public static List<double> srdCSVFile()
    {
        List<double> records = new List<double>();

        try
        {
            string filePath = Application.streamingAssetsPath + "/expertTrial_short.csv";
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
