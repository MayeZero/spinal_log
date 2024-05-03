using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrailData 
{
    public List<double> forceTrail;

    public TrailData()
    {
        this.forceTrail = new List<double>();
    }

    public void GenerateRandomData()
    {
        if (forceTrail.Count > 300)
        {
            ClearData();
        }
        
        double randomValue = Random.Range(0.0f, 25.0f);
        forceTrail.Add(randomValue);

        Debug.Log("Random data generated:" + randomValue);
        Debug.Log("Data number: " + forceTrail.Count);
        
    }

    private void ClearData()
    {
        this.forceTrail.Clear();
    }
}