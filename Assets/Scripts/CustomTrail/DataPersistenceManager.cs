using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using System.IO;


public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    public int fileCount = 0;

    private TrailData trailData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
        //printAllFiles();
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        fileCount = getAllFiles().Length;
        LoadGraph();
        //deleteAllFiles();
        
    }

    public void NewGraph()
    {
        this.trailData = new TrailData();
        Debug.Log("New trail initialised");
    }

    public void LoadGraph()
    {
        this.trailData = dataHandler.Load();

        if(this.trailData == null)
        {
            Debug.Log("No data was found. Initialising data to defaults");
            NewGraph();
        }
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.loadData(trailData);
        }

        Debug.Log("Loaded trailData = " + trailData.forceTrail.Count);
    }

    public void SaveGraph()
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref trailData);
        }
        Debug.Log("Saved trailData = " + trailData.forceTrail.Count);

        dataHandler.Save(trailData);
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGraph(); 
    //}

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }


    public void setFileName(string filename = null)
    {
        if (filename == null)
        {
            filename = fileName;
        }

        this.dataHandler.setPath(filename);
    }


    public string getFileName()
    {
        return this.dataHandler.getPath();
    }

    public string makeNewFile(int index)
    {
        string filename = "data" + getFileCount() + ".csv";
        return filename;
    }

    public int getFileCount()
    {
        return getAllFiles().Length;
    }

    public string[] getAllFiles()
    {
        // Get the persistent data path
        string path = Application.persistentDataPath;

        // Check if the directory exists
        if (Directory.Exists(path))
        {
            // Get all files in the directory
            string[] files = Directory.GetFiles(path);
            return files;
        }
        else
        {
            Debug.Log("Directory does not exist.");
        }
        return null;
    }

    public void printAllFiles()
    {
        string[] files = getAllFiles();
        if (files != null)
        {
            // Loop through and print each file path
            foreach (string file in files)
            {
                Debug.Log("File: " + file);
            }
        }
        else
        {
            Debug.Log("Directory does not exist.");
        }
    }

    public void deleteAllFiles()
    {
        // Get all files in the directory
        string[] files = Directory.GetFiles(Application.persistentDataPath);

        // Loop through and delete each file
        foreach (string file in files)
        {
            try
            {
                File.Delete(file);
                Debug.Log("Deleted file: " + file);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error deleting file: " + file + "\n" + e.Message);
            }
        }
    }


}
