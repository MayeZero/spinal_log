using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<double> randomData { get; private set; } = new List<double>();

    private void Start()
    {
        randomData = new List<double>();
    }

    /// <summary>
    /// Use this as a data reader at this stage
    /// </summary>

    public void ClearData()
    {
        randomData.Clear();
    }
}
