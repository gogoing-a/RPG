using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barnpc : NPC
{
    public static Barnpc _instance;
    public TweenPosition questtween;
    public bool isintask=false;//是否在任务中
    public int killcount=0;//表示杀死了几只狼，表示任务进度

    public GameObject acceptbtn;
    public GameObject okbptn;
    public GameObject canclebtn;
    public UILabel desLabel;

    private playerstatus status;
    private AudioSource auds;
    // Start is called before the first frame update


    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        status = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
        auds=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showtaskdes()
    {
        desLabel.text = "任务:\n杀死10只狼\n\n奖励:\n1000金币";
        okbptn.SetActive(false);
        acceptbtn.SetActive(true);
        canclebtn.SetActive(true);
    }

    void showtaskprojress()
    {
        desLabel.text = "任务:\n你已经杀死了" + killcount + "\\10只狼\n\n奖励:\n1000金币";
        okbptn.SetActive(true);
        acceptbtn.SetActive(false);
        canclebtn.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            auds.Play();
            if(isintask)
            {
                showtaskprojress();
            }
            else
            {
                showtaskdes();
            }
            showquest();
        }
    }

    private void showquest()
    {
        questtween.gameObject.SetActive(true);
        questtween.PlayForward();
    }

    private void Hidequest()
    {
        questtween.PlayReverse();
    }

    public void OnKillWolf()
    {
        if(isintask)
        {
            killcount++;
        }
    }

    public void OncloseButtonClick()
    {
        Hidequest();
    }

    public void OnAcceptButtonClick()
    {
        showtaskprojress();
        isintask = true;
    }

    public void OnCancleButtonClick()
    {
        Hidequest();
    }

    public void OnOkButtonClick()
    {
        if(killcount>=10)//任务完成
        {
            inventory._instance.AddCoint(1000);
            killcount = 0;
            showtaskdes();
        }
        else//任务未完成
        {
            Hidequest();
        }
    }
}
