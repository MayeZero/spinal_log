using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class ShowCustomGraphYT : MonoBehaviour, IDataPersistence
{

    [SerializeField] LineChart chart;

    private List<double> forceTrail;

    // Start is called before the first frame update
    void Start()
    {
        forceTrail = new List<double>();
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

    

    public void loadData(TrailData data)
    {
        this.forceTrail = data.forceTrail;
    }

    public void SaveData(ref TrailData data)
    {
        data.forceTrail = this.forceTrail;
    }
}
