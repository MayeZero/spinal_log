using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class ShowingGraphPreset : MonoBehaviour
{

    [SerializeField] LineChart chart;
    
    private List<float> forceTrail;
    private List<float> realTimeForceInputPreset;
    

    // Start is called before the first frame update
    void Start()
    {
        //forceTrail = new List<float>();
        //forceTrail = csvReader.srdCSVFile();
        //realTimeForceInputPreset = new List<float>();
        InitChartWithTrail();
    }

    private void Awake()
    {
        this.forceTrail = csvReader.srdCSVFile();
        realTimeForceInputPreset = new List<float>();
        //Debug.Log(forceTrail.Count);
        //InitChartWithTrail();
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
        chart.EnsureChartComponent<Title>().text = "Preset Trail";

        chart.EnsureChartComponent<Tooltip>().show = true;
        chart.EnsureChartComponent<Legend>().show = false;

        var xAxis = chart.EnsureChartComponent<XAxis>();
        var yAxis = chart.EnsureChartComponent<YAxis>();
        xAxis.show = true;
        yAxis.show = true;
        // X Axis settings //
        xAxis.type = Axis.AxisType.Value;
        //xAxis.type = Axis.AxisType.Time;
        xAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        xAxis.min = 0;
        xAxis.max = forceTrail.Count;
        xAxis.interval = forceTrail.Count / 5;
        //xAxis.type = Axis.AxisType.Category;
        xAxis.splitNumber = forceTrail.Count;
        xAxis.boundaryGap = false;
        // Y Axis settings //
        yAxis.type = Axis.AxisType.Value;
        yAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        yAxis.min = 0;
        yAxis.max = 100;
        yAxis.interval = yAxis.max / 5;
        // yAxis.splitNumber = forceTrail.Count;
        // yAxis.boundaryGap = false;
        

        chart.RemoveData();
        var serie0 = chart.AddSerie<Line>("PresetTrail");
        var serie1 = chart.AddSerie<Line>("InputForce2");
        //var serie1 = chart.GetSerie<Line>(1);
        serie0.symbol.show = false;
        serie1.symbol.show = false;
        for (int i = 0; i < forceTrail.Count; i++)
        {
            //chart.AddXAxisData(Time.time);
            //chart.AddXAxisData("x" + i);
            //chart.AddData("CustomTrail", sldataManager.dataFromCustomTrail[i]);
            chart.AddData("PresetTrail", forceTrail[i]);
            //chart.AddData("RealTimeData", dataManager.randomData[i]);
        }
    }

    public void addRealTimeDataToGraph(float inputForce)
    {
        //var chart = gameObject.GetComponent<LineChart>();
        var serie2 = chart.GetSerie("InputForce2");
        if (this.realTimeForceInputPreset.Count > forceTrail.Count)
        {
            realTimeForceInputPreset.Clear();
            serie2.ClearData();
        }
        else
        {
            realTimeForceInputPreset.Add(inputForce);
            chart.AddData("InputForce2", inputForce);
        }

    }

    // public void addCustomTrailToGraph()
    // {
        
    //     //var chart = gameObject.GetComponent<LineChart>();
    //     var serie0 = chart.GetSerie("PresetTrail");
    //     var serie1 = chart.GetSerie("InputForce");
    //     serie0.ClearData();
    //     serie1.ClearData();
        
    //     for (int i = 0; i < forceTrail.Count; i++)
    //     {
    //         chart.AddData("PresetTrail", forceTrail[i]);

    //     }
    // }

    public void cleanRealTimeData()
    {
        realTimeForceInputPreset.Clear();
    }

    // public void cleanGraph()
    // {
    //     //var chart = gameObject.GetComponent<LineChart>();
    //     var serie0 = chart.GetSerie("PresetTrail");
    //     var serie1 = chart.GetSerie("InputForce");
    //     serie0.ClearData();
    //     serie1.ClearData();
    // }

    // public void loadData(TrailData data)
    // {
    //     this.forceTrail = data.forceTrail;
    //     Debug.Log("DataLoaded");
    // }

    // public void SaveData(ref TrailData data)
    // {
    //     // save real time force trail to data.forceTrail
    //     data.forceTrail = this.realTimeForceInput;
    // }
}
