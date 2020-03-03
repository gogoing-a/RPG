using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Swordman,//剑士
    Megiclan,//魔法师
}

public class playerstatus : MonoBehaviour
{
    public HeroType heroType;

    public int level = 1;//等级

    public float exp = 0;//经验
    public string name = "默认";
    public int hp = 100;//最大值
    public int mp = 100;

    public float hp_remain = 100;
    public float mp_remain = 100;


    public float attack=20;
    public int attack_plus = 0;
    public float def = 20;
    public int def_plu=0;
    public float speed =20;
    public int speed_plus = 0;

    public int point_remain = 0;//剩余技能点数

    private void Start()
    {
        GetExp(0);
    }

    public void GetDrug(int hp,int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if(hp_remain>this.hp)
        {
            hp_remain = this.hp;
        }
        if(mp_remain>this.mp)
        {
            mp_remain = this.mp;
        }
        headstatusUI._instance.UpdateShow();
    }

    public bool GetPoint(int point=1)
    {
        if(point_remain>=point)
        {
            point_remain -= point;
            return true;
        }
        return false;
    }

    public void GetExp(int exp)
    {
        this.exp += exp;
        int total_exp = 100 + level * 30;
        while(this.exp>total_exp)//升级
        {
            this.level++;
            this.exp -= total_exp;
            total_exp = 100 + level * 30;
        }
        expbar._instance.setvalue(this.exp / total_exp);  
    }

    public bool TakeMP(int count)
    {
        if(mp_remain>=count)
        {
            mp_remain -= count;
            headstatusUI._instance.UpdateShow();
            return true;
        }
        else
        {
            return false;
        }
    }
}
