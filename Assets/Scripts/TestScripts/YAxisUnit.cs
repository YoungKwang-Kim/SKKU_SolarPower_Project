using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class YAxisUnit : MonoBehaviour
{
    ChartLabel[] yAxisLabel;
    ChartText[] yAxisText;

    private void Start()
    {
        yAxisLabel = GetComponentsInChildren<ChartLabel>();
        for (int i = 0; i < yAxisLabel.Length; i++)
        {
            yAxisLabel[i].enabled = false;
        }
    }
}
