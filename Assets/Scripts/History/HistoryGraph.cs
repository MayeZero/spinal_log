using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class HistoryGraph : MonoBehaviour
{

    [SerializeField] LineChart chart;
    [SerializeField] ButtonsHandllers_History buttonsHandllers;
    
    private List<float> forceTrail;
    //private List<float> realTimeForceInput;
    private float previous_filtered_value = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //forceTrail = new List<float>();
        //realTimeForceInput = new List<float>();
    }

    private void Awake()
    {
        this.forceTrail = FileHandler.readCSVFile(Application.streamingAssetsPath, "expertTrial.csv");
        InitChartWithTrail();
    }

    public void InitChartWithTrail()
    {
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
        yAxis.max = 30;
        yAxis.interval = yAxis.max / 5;
        // yAxis.splitNumber = forceTrail.Count;
        // yAxis.boundaryGap = false;


        chart.RemoveData();
        var serie0 = chart.AddSerie<Line>("PresetTrail");
        //var serie1 = chart.AddSerie<Line>("InputForce");

        serie0.symbol.show = false;
        //serie1.symbol.show = false;
        for (int i = 0; i < forceTrail.Count; i++)
        {
            chart.AddData("PresetTrail", forceTrail[i]);
        }
    }


    public void addCustomTrailToGraph()
    {
        
        //var chart = gameObject.GetComponent<LineChart>();
        var serie0 = chart.GetSerie("PresetTrail");
        //var serie1 = chart.GetSerie("InputForce");
        serie0.ClearData();
        //serie1.ClearData();
        
        for (int i = 0; i < forceTrail.Count; i++)
        {
            chart.AddData("PresetTrail", forceTrail[i]);

        }
    }

    //public void cleanRealTimeData()
    //{
    //    realTimeForceInput.Clear();
    //}

    public void cleanGraph()
    {
        //var chart = gameObject.GetComponent<LineChart>();
        var serie0 = chart.GetSerie("PresetTrail");
        //var serie1 = chart.GetSerie("InputForce");
        serie0.ClearData();
        //serie1.ClearData();
    }

    //public void loadData(string filename)
    //{
    //    this.forceTrail = csvReader.srdCSVFile(filename);
    //}

    public void setData(string folder, string filename)
    {
        this.forceTrail = FileHandler.readCSVFile(folder, filename);
        InitChartWithTrail();
        //addCustomTrailToGraph();
    }

    //public void resetToFile(string filename)
    //{
    //    this.filename = filename;
    //    this.forceTrail = csvReader.srdCSVFile(filename);
    //    realTimeForceInputPreset = new List<float>();
    //    InitChartWithTrail();
    //}

    //public void loadData(TrailData data)
    //{
    //    this.forceTrail = data.forceTrail;
    //    Debug.Log("DataLoaded");
    //}

    //public void SaveData(ref TrailData data)
    //{
    //    // save real time force trail to data.forceTrail
    //    data.forceTrail = this.realTimeForceInput;
    //}


    //public float smoothData(float newData, float alpha, float previous_filtered_value)
    //{
    //    float filtered_value = alpha * newData + (1 - alpha) * previous_filtered_value;
    //    return filtered_value;
    //}
}
