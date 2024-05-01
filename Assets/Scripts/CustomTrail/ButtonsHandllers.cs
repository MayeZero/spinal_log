using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsHandllers : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnSaveButtonClicked()
    {
        DataPersistenceManager.instance.SaveGraph();

    }

    public void OnLoadButtonClicked()
    {
        DataPersistenceManager.instance.LoadGraph();
    }

    public void OnClearButtonClicked()
    {
        DataPersistenceManager.instance.NewGraph();
    }
}
