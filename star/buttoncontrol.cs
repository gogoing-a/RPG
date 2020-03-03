using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttoncontrol : MonoBehaviour
{
    public void newgame()
    {
        PlayerPrefs.SetInt("DataFromSave", 0);
        SceneManager.LoadScene(1);
    }

    public void loadgame()
    {
        PlayerPrefs.SetInt("DataFromSave", 1);
    }
}
