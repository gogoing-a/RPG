using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorydes : MonoBehaviour
{
    public static inventorydes _instance;
    private UILabel label;
    private float timer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        this.gameObject.SetActive(false);
        label = this.GetComponentInChildren<UILabel>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy==true)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void show(int id)
    {
        this.gameObject.SetActive(true);timer = 0.1f;
        transform.position= UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        objectinfo info = objectsinfo._instance.GetObjectInFoById(id);
        string des = "";
        switch (info.type)
        {
            case objectType.Drug:
                des=GetDrugDse(info);
                break;
            case objectType.Equip:
                des = GetEquipDes(info);
                break;
            case objectType.Mat:
                break;
        }
        label.text = des;
    }

    private string GetDrugDse(objectinfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        str += "+HP: " + info.hp + "\n";
        str += "+MP: " + info.mp + "\n";
        str += "出售价: " + info.price_sell + "\n";
        str += "购买价: " + info.price_buy;

        return str;
    }

    private string GetEquipDes(objectinfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        switch (info.dressType)
        {
            case DressType.Headgear:
                str += "穿戴类型：头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型：盔甲\n";
                break;
            case DressType.RightHand:
                str += "穿戴类型：右手\n";
                break;
            case DressType.LeftHand:
                str += "穿戴类型：左手\n";
                break;
            case DressType.Shoe:
                str += "穿戴类型：鞋\n";
                break;
            case DressType.Accessory:
                str += "穿戴类型：饰品\n";
                break;
        }

        switch (info.applictiontype)
        {
            case ApplicationType.Swordman:
                str += "适用类型：剑士\n";
                break;
            case ApplicationType.Megiclan:
                str += "适用类型：魔法师\n";
                break;
            case ApplicationType.Commoon:
                str += "适用类型：通用\n";
                break;
        }

        str += "伤害值: " + info.attack + "\n";
        str += "防御值: " + info.def + "\n";
        str += "速度值: " + info.speed + "\n";
        str += "出售价: " + info.price_sell + "\n";
        str += "购买价: " + info.price_buy;
        return str;
    }
}