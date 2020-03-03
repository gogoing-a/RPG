using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class status : MonoBehaviour
{
    public static status _instace;

    private TweenPosition tween;
    private bool isshow = false;
    public UILabel attacklable;
    public UILabel deflable;
    public UILabel speedlable;
    public UILabel pointremainlable;
    public UILabel summarylable;

    public GameObject attackButtonGo;
    public GameObject defButtonGo;
    public GameObject speedButtonGo;

    private playerstatus ps;
    

    // Start is called before the first frame update
    void Awake()
    {
        _instace = this;
        tween = this.GetComponent<TweenPosition>();
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
    }

    public void TransformState()
    {
        Updateshow();
        if(isshow==false)
        {
            tween.PlayForward();
            isshow = true;
        }
        else
        {
            tween.PlayReverse();
            isshow = false;
        }
    }

    void Updateshow()//更新显示
    {
        attacklable.text = ps.attack + " + " + ps.attack_plus;
        deflable.text = ps.def + " + " + ps.def_plu;
        speedlable.text = ps.speed + " + " + ps.speed_plus;

        pointremainlable.text = ps.point_remain.ToString();

        summarylable.text = "伤害：" + (ps.attack + ps.attack_plus)
            + " " + "防御：" + (ps.def + ps.def_plu)
            + " " + "速度：" + (ps.speed + ps.speed_plus);

        if(ps.point_remain>0)
        {
            attackButtonGo.SetActive(true);
            defButtonGo.SetActive(true);
            speedButtonGo.SetActive(true);
        }
        else
        {
            attackButtonGo.SetActive(false);
            defButtonGo.SetActive(false);
            speedButtonGo.SetActive(false);
        }

    }

    public void onattackplusclick()
    {
        bool success = ps.GetPoint();
        if(success)
        {
            ps.attack_plus++;
            Updateshow();
        }
    }

    public void ondefplusclick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.def_plu++;
            Updateshow();
        }
    }

    public void onspeedplusclick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.speed_plus++;
            Updateshow();
        }
    }
}
