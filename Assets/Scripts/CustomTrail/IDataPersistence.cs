using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void loadData(TrailData data);

    void SaveData(ref TrailData data);
}
