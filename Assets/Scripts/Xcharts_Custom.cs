using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;

public class Xcharts_Custom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }

        chart.SetSize(580, 300);//代码动态设置尺寸，或直接操作chart.rectTransform，或直接在Inspector上改

        var title = chart.EnsureChartComponent<Title>();
        title.text = "Simple Line";

        var tooltip = chart.EnsureChartComponent<Tooltip>();
        tooltip.show = true;

        var legend = chart.EnsureChartComponent<Legend>();
        legend.show = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
