using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{

    public GameObject magicianPrefab;
    public GameObject swordmanPrefab;

    private void Awake()
    {

        int selectindex = PlayerPrefs.GetInt("selectcharaindex");
        string name = PlayerPrefs.GetString("name");
        GameObject go = null;

        if(selectindex==0)
        {
            go=GameObject.Instantiate(magicianPrefab);
        }
        else if(selectindex==1)
        {
            go=GameObject.Instantiate(swordmanPrefab);
        }
        go.GetComponent<playerstatus>().name = name;
    }
}
