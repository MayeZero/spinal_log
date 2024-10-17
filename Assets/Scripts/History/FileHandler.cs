using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void deleteFile(string folder, string file)
    {
        string path = Path.Combine(folder, file);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Deleted file: " + path);
        }
    }

    public static void renameAllFiles(string folderPath)
    {
        string baseFileName = "data";
        if (Directory.Exists(folderPath))
        {
            // Get all .csv files in the folder
            string[] files = Directory.GetFiles(folderPath, "*.csv");

            for (int i = 0; i < files.Length; i++)
            {
                string oldFilePath = files[i];
                string extension = Path.GetExtension(oldFilePath);
                string newFileName = baseFileName + (i + 1) + extension; // Renaming to data1, data2, etc.
                string newFilePath = Path.Combine(folderPath, newFileName);

                // Rename the file
                File.Move(oldFilePath, newFilePath);
                Debug.Log("Renamed file: " + oldFilePath + " to " + newFilePath);
            }
        }
        else
        {
            Debug.LogError("Folder does not exist: " + folderPath);
        }
    }


    public static List<float> readCSVFile(string datadir, string filename)
    {
        List<float> records = new List<float>();

        string fullPath = Path.Combine(datadir, filename);

        if (File.Exists(fullPath))
        {
            try
            {
                string[] lines = File.ReadAllLines(fullPath);
                Debug.Log(lines);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (float.TryParse(lines[i], out float value))
                    {
                        records.Add(value);
                    }
                    else
                    {
                        Debug.LogWarning($"Skipping invalid data at line {i}: {lines[i]}");
                    }

                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return records;
    }
}
