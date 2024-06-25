using System.Collections.Generic;
using UnityEngine;
#if INPUT_SYSTEM_ENABLED
using Input = XCharts.Runtime.InputHelper;
#endif
using XCharts.Runtime;
using System;
using System.Collections;
using System.IO;
using System.Linq;

public class ShowingGraph : MonoBehaviour
{
    private List<double> csvData;
    [SerializeField] DataManager dataManager;

    void Awake()
    {
        //dataManager = GetComponent<DataManager>();
        //csvData = csvReader.srdCSVFile();
        AddTrailData();
        //AddRealTimeData();

    }

    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    dataManager.GenerateRandomData();
        //    AddRealTimeData();
        //}
    }

    void AddTrailData()
    {
        var chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }
        chart.EnsureChartComponent<Title>().show = false;
        chart.EnsureChartComponent<Title>().text = "Line Simple";

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
        var serie0 = chart.AddSerie<Line>("Trail");
        var serie1 = chart.AddSerie<Line>("RealTimeData");
        //var serie1 = chart.GetSerie<Line>(1);
        serie0.symbol.show = false;
        serie1.symbol.show = false;
        for (int i = 0; i < xAxis.splitNumber; i++)
        {
            //chart.AddXAxisData(Time.time);
            chart.AddXAxisData("x" + i);
            chart.AddData("Trail", csvData[i]);
            //chart.AddData("RealTimeData", dataManager.randomData[i]);
        }
    }

    void AddRealTimeData()
    {
        var chart = gameObject.GetComponent<LineChart>(); 
        var serie1 = chart.GetSerie("RealTimeData");
        


        if (serie1.dataCount > 300)
        {
            serie1.ClearData();
        } else
        {
            chart.AddData("RealTimeData", dataManager.randomData.Last());
        }

        
    }

}
