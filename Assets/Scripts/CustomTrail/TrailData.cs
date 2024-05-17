using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrailData 
{
    public List<float> forceTrail;

    public TrailData()
    {
        this.forceTrail = new List<float>();
    }

    public void GenerateRandomData()
    {
        if (forceTrail.Count > 300)
        {
            ClearData();
        }
        
        float randomValue = Random.Range(0.0f, 25.0f);
        forceTrail.Add(randomValue);

        Debug.Log("Random data generated:" + randomValue);
        Debug.Log("Data number: " + forceTrail.Count);
        
    }

    private void ClearData()
    {
        this.forceTrail.Clear();
    }
}