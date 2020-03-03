using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsinfo : MonoBehaviour
{
    public static objectsinfo _instance;

    public TextAsset objectsInfoListText;

    private Dictionary<int, objectinfo> objectinfodict = new Dictionary<int, objectinfo>();
    private void Awake()
    {
        _instance = this;
        ReadInfo();
    }

    public objectinfo GetObjectInFoById(int id)
    {
        objectinfo info = null;

        objectinfodict.TryGetValue(id, out info);

        return info;
    }

    void ReadInfo()
    {
        string text = objectsInfoListText.text;
        string[] strArray = text.Split('\n');

        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            objectinfo info = new objectinfo();

            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string icon_name = proArray[2];
            string str_type = proArray[3];
            objectType type = objectType.Drug;

            switch (str_type)
            {
                case "Drug":
                    type = objectType.Drug;
                    break;
                case "Equip":
                    type = objectType.Equip;
                    break;
                case "Mat":
                    type = objectType.Mat;
                    break;
            }
            info.id = id;
            info.name = name;
            info.icon_name = icon_name;
            info.type = type;
            if (type == objectType.Drug)
            {
                int hp = int.Parse(proArray[4]);
                int mp = int.Parse(proArray[5]);
                int price_sell = int.Parse(proArray[6]);
                int price_buy = int.Parse(proArray[7]);
                info.hp = hp;
                info.mp = mp;
                info.price_buy = price_buy;
                info.price_sell = price_sell;

            }
            else if (type == objectType.Equip)
            {
                info.attack = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.price_sell = int.Parse(proArray[9]);
                info.price_buy = int.Parse(proArray[10]);
                string str_dresstype = proArray[7];
                switch (str_dresstype)
                {
                    case "Headgear":
                        info.dressType = DressType.Headgear;
                        break;
                    case "Armor":
                        info.dressType = DressType.Armor;
                        break;
                    case "RightHand":
                        info.dressType = DressType.RightHand;
                        break;
                    case "LeftHand":
                        info.dressType = DressType.LeftHand;
                        break;
                    case "Shoe":
                        info.dressType = DressType.Shoe;
                        break;
                    case "Accessory":
                        info.dressType = DressType.Accessory;
                        break;
                }
                string str_apptype = proArray[8];
                switch (str_apptype)
                {
                    case "Swordman":
                        info.applictiontype = ApplicationType.Swordman;
                        break;
                    case "Magician":
                        info.applictiontype = ApplicationType.Megiclan;
                        break;
                    case "Common":
                        info.applictiontype = ApplicationType.Commoon;
                        break;
                }

            }
            objectinfodict.Add(id, info);//添加到字典中，id为key,可以通过id查找到info
        }
        //id
        //名称
        //icon名称
        //类型（药品drag）
        //加血量值
        //加魔法值
        //出售价
        //购买

    }
}

 public enum objectType
{
    Drug,
    Equip,
    Mat
}

public enum DressType
{
    Headgear,
    Armor,
    RightHand,
    LeftHand,
    Shoe,
    Accessory
}

public enum ApplicationType
{
    Swordman,//剑士
    Megiclan,//魔法师
    Commoon //通用
}

public class objectinfo
{
    public int id;
    public string name;
    public string icon_name;//图片名称
    public objectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;

    public int attack;
    public int def;
    public int speed;
    public DressType dressType;
    public ApplicationType applictiontype;
}