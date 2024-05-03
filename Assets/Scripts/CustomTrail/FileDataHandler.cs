using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;

    }

    public TrailData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        TrailData loadedTrailData = new TrailData();
        if (File.Exists(fullPath))
        {
            try
            {
                string[] lines = File.ReadAllLines(fullPath);
                Debug.Log(lines);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (double.TryParse(lines[i], out double value))
                    {
                        loadedTrailData.forceTrail.Add(value);
                    }
                    else
                    {
                        Debug.LogWarning($"Skipping invalid data at line {i}: {lines[i]}");
                    }

                    Debug.Log($"Read {loadedTrailData.forceTrail.Count} data points from {fullPath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedTrailData;
    }

    public void Save(TrailData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Force");

            foreach (double value in data.forceTrail)
            {
                stringBuilder.AppendLine(value.ToString());
            }
            File.WriteAllText(fullPath, stringBuilder.ToString());
            Debug.Log($"Data saved to {fullPath}");

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            
        }
    }

}
