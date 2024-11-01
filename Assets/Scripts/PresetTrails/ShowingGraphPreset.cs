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
    private string filename = "expertTrial_short.csv";


    // Start is called before the first frame update
    void Start()
    {
        InitChartWithTrail();
    }

    private void Awake()
    {
        this.forceTrail = csvReader.srdCSVFile(filename);
        realTimeForceInputPreset = new List<float>();
    }

    // Initialize the chart with trail data
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
        var serie1 = chart.AddSerie<Line>("InputForce2");
        
        serie0.symbol.show = false;
        serie1.symbol.show = false;
        for (int i = 0; i < forceTrail.Count; i++)
        {
            chart.AddData("PresetTrail", forceTrail[i]);
        }
    }
    
    
    // Add real-time force data to the graph
    public void addRealTimeDataToGraph(float inputForce)
    {
        var serie2 = chart.GetSerie("InputForce2");
        if (this.realTimeForceInputPreset.Count > forceTrail.Count)
        {
            // Clear data if it exceeds the preset trail count
            realTimeForceInputPreset.Clear();
            serie2.ClearData();
        }
        else
        {
            // Add new force data to the list and chart
            realTimeForceInputPreset.Add(inputForce);
            chart.AddData("InputForce2", inputForce);
        }

    }

    // Clear real-time force data   
    public void cleanRealTimeData()
    {
        realTimeForceInputPreset.Clear();
    }


    public void resetToFile(string filename)
    {
        this.filename = filename;
        this.forceTrail = csvReader.srdCSVFile(filename);
        realTimeForceInputPreset = new List<float>();
        InitChartWithTrail();
    }

}
