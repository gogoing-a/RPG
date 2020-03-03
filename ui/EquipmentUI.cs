using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{

    public static EquipmentUI _instance;
    private bool isshow = false;
    private TweenPosition tween;

    private GameObject heargear;
    private GameObject armor;
    private GameObject righthand;
    private GameObject lefthand;
    private GameObject shoe;
    private GameObject accessoty;
    private playerstatus ps;
    public int attack = 0;
    public int def = 0;
    public int speed = 0;


    public GameObject equipmentItem;
    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();

        heargear = transform.Find("headgear").gameObject;
        armor = transform.Find("armor").gameObject;
        righthand = transform.Find("right-hand").gameObject;
        lefthand = transform.Find("left-hand").gameObject;
        shoe = transform.Find("shoe").gameObject;
        accessoty = transform.Find("accessory").gameObject;

    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
    }

    public void transformstate()
    {
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

    public bool Dress(int id)
    {
        objectinfo info = objectsinfo._instance.GetObjectInFoById(id);
        if(info.type!=objectType.Equip)
        {
            return false;//穿戴不成功
        }

        if(ps.heroType==HeroType.Megiclan)
        {
            if(info.applictiontype==ApplicationType.Swordman)
            {
                return false;
            }
        }

        if(ps.heroType == HeroType.Swordman)
        {
            if (info.applictiontype == ApplicationType.Megiclan)
            {
                return false;
            }
        }
        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = heargear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = righthand;
                break;
            case DressType.LeftHand:
                parent = lefthand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessoty;
                break;
        }
        Equipmentitem item = parent.GetComponentInChildren<Equipmentitem>();
        if(item!=null)//说明穿了同样类型的装备
        {
            inventory._instance.GetId(item.id);
            item.setinfo(info);
        }
        else
        {
            GameObject itemGo=NGUITools.AddChild(parent, equipmentItem);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<Equipmentitem>().setinfo(info);
        }
        UpdateProperty();
        return true;
    }

    public void tankeoff(int id,GameObject go)
    {
        inventory._instance.GetId(id);
        GameObject.Destroy(go);
        UpdateProperty();
    }

    private void UpdateProperty()
    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;

        Equipmentitem headgearitem= heargear.GetComponentInChildren<Equipmentitem>();        
        plusProperty(headgearitem);
        Equipmentitem armoritem = heargear.GetComponentInChildren<Equipmentitem>();
        plusProperty(armoritem);
        Equipmentitem lefthanditem = heargear.GetComponentInChildren<Equipmentitem>();
        plusProperty(lefthanditem);
        Equipmentitem righthanditem = heargear.GetComponentInChildren<Equipmentitem>();
        plusProperty(righthanditem);
        Equipmentitem shoeitem = heargear.GetComponentInChildren<Equipmentitem>();
        plusProperty(shoeitem);
        Equipmentitem accessoryitem = heargear.GetComponentInChildren<Equipmentitem>();
        plusProperty(accessoryitem);
    }

    void plusProperty(Equipmentitem item)
    {
        if (item != null)
        {
            objectinfo equipinfo = objectsinfo._instance.GetObjectInFoById(item.id);
            this.attack += equipinfo.attack;
            this.def += equipinfo.def;
            this.speed += equipinfo.speed;
        }
    }
}
