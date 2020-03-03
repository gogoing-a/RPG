using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillUI : MonoBehaviour
{
    public static skillUI _instance;

    private TweenPosition tween;
    private bool isshow = false;
    private playerstatus ps;

    public UIGrid grid;
    public GameObject skillitemPrefab;
    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        
    }
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
        int[] idlist = null;
        switch (ps.heroType)
        {
            case HeroType.Megiclan:
                idlist = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idlist = swordmanSkillIdList;
                break;
        }
        foreach(int id in idlist)
        {
            GameObject itemGo= NGUITools.AddChild(grid.gameObject, skillitemPrefab);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<skillitem>().Setld(id);
        }
    }

    public void Transfromstate()
    {
        if(isshow)
        {
            tween.PlayForward();
            isshow = false;
            //Updateshow();
        }
        else
        {
            tween.PlayReverse();
            isshow = true;
            Updateshow();
        }
    }

    private void Updateshow()
    {
        skillitem[] items = this.GetComponentsInChildren<skillitem>();
        foreach(skillitem item in items)
        {
            item.UpdateShow(ps.level);
        }
    }
}
