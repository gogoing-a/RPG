using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressanykey : MonoBehaviour
{

    private bool isanykeydown=false;
    private GameObject buttoncontrol;
    // Start is called before the first frame update
    void Start()
    {
        buttoncontrol = this.transform.parent.Find("buttoncontrol").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isanykeydown==false)
        {
            if(Input.anyKey)
            {
                showbutton();
            }
        }
    }

    private void showbutton()
    {
        buttoncontrol.SetActive(true);
        this.transform.gameObject.SetActive(false);
        isanykeydown = true;
    }
}
