using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }
}
