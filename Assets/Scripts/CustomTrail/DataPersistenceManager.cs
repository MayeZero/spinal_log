using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private TrailData trailData;

    private List<IDataPersistence> dataPersistenceObjects;
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
    }

    private void OnApplicationQuit()
    {
        SaveGraph(); 
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }



}
