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
}