using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    int fileCount = 1;

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
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGraph();
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

    public string makeNewFile()
    {
        string filename = "data" + fileCount + ".csv";
        fileCount += 1;
        return filename;
    }

    public int getFileCount()
    {
        return fileCount;
    }


}
