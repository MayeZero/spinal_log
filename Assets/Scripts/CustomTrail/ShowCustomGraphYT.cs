using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class ShowCustomGraphYT : MonoBehaviour, IDataPersistence
{

    [SerializeField] LineChart chart;
    
    private List<double> forceTrail;
    private List<double> realTimeForceInput;
    

    // Start is called before the first frame update
    void Start()
    {
        forceTrail = new List<double>();
        realTimeForceInput = new List<double>();
    }

    private void Awake()
    {
        //dataManager = GetComponent<DataManager>();
        //csvData = csvReader.srdCSVFile();
        InitChartWithTrail();
        //AddRealTimeData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GenerateRandomData();
            addRealTimeDataToGraph();
        }
    }
    void InitChartWithTrail()
    {
        //var chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
            Debug.Log("Chart Initialised");
        }
        chart.EnsureChartComponent<Title>().show = true;
        chart.EnsureChartComponent<Title>().text = "Custom Trail";

        chart.EnsureChartComponent<Tooltip>().show = true;
        chart.EnsureChartComponent<Legend>().show = false;

        var xAxis = chart.EnsureChartComponent<XAxis>();
        var yAxis = chart.EnsureChartComponent<YAxis>();
        xAxis.show = true;
        yAxis.show = true;
        xAxis.type = Axis.AxisType.Value;
        //xAxis.type = Axis.AxisType.Time;
        xAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        xAxis.min = 0;
        xAxis.max = 300;
        xAxis.interval = 30;
        //xAxis.type = Axis.AxisType.Category;
        yAxis.type = Axis.AxisType.Value;

        xAxis.splitNumber = 300;
        xAxis.boundaryGap = false;

        chart.RemoveData();
        var serie0 = chart.AddSerie<Line>("CustomTrail");
        var serie1 = chart.AddSerie<Line>("InputForce");
        //var serie1 = chart.GetSerie<Line>(1);
        serie0.symbol.show = false;
        serie1.symbol.show = false;
        for (int i = 0; i < xAxis.splitNumber; i++)
        {
            //chart.AddXAxisData(Time.time);
            //chart.AddXAxisData("x" + i);
            //chart.AddData("CustomTrail", sldataManager.dataFromCustomTrail[i]);
            chart.AddData("CustomTrail");
            //chart.AddData("RealTimeData", dataManager.randomData[i]);
        }
    }

    //private void generateRandomData()
    //{
    //    if (randomData.Count > 300)
    //    {
    //        ClearData();
    //    }
        
    //    double randomValue = Random.Range(0.0f, 25.0f);
    //    randomData.Add(randomValue);

    //    Debug.Log("Random data generated:" + randomValue);
    //    Debug.Log("Data number: " + randomData.Count);
        
    //}

    private void addRealTimeDataToGraph()
    {
        //var chart = gameObject.GetComponent<LineChart>();
        var serie1 = chart.GetSerie("InputForce");

        if (serie1.dataCount > 300)
        {
            serie1.ClearData();
        }
        else
        {
            chart.AddData("InputForce", realTimeForceInput.Last());
        }
    }

    private void GenerateRandomData()
    {
        if (realTimeForceInput.Count > 300)
        {
            realTimeForceInput.Clear();
        }

        double randomValue = Random.Range(0.0f, 25.0f);
        realTimeForceInput.Add(randomValue);

        Debug.Log("Random data generated:" + randomValue);
        Debug.Log("Data number: " + realTimeForceInput.Count);

    }


    public void addCustomTrailToGraph()
    {
        
        //var chart = gameObject.GetComponent<LineChart>();
        var serie0 = chart.GetSerie("CustomTrail");
        var serie1 = chart.GetSerie("InputForce");
        serie0.ClearData();
        serie1.ClearData();
        
        for (int i = 0; i < forceTrail.Count; i++)
        {
            chart.AddData("CustomTrail", forceTrail[i]);

        }
    }

    public void cleanRealTimeData()
    {
        realTimeForceInput.Clear();
    }

    public void cleanGraph()
    {
        //var chart = gameObject.GetComponent<LineChart>();
        var serie0 = chart.GetSerie("CustomTrail");
        var serie1 = chart.GetSerie("InputForce");
        serie0.ClearData();
        serie1.ClearData();
    }

    public void loadData(TrailData data)
    {
        this.forceTrail = data.forceTrail;
        Debug.Log("DataLoaded");
    }

    public void SaveData(ref TrailData data)
    {
        // save real time force trail to data.forceTrail
        data.forceTrail = this.realTimeForceInput;
    }
}
