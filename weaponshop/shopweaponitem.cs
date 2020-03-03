using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopweaponitem : MonoBehaviour
{

    private int id;
    private objectinfo info;
    private UISprite Icon_sprite;
    private UILabel name_label;
    private UILabel effect_label;
    private UILabel pricesell_label;

    private void Awake()
    {
        Icon_sprite = transform.Find("icon").GetComponent<UISprite>();
        name_label = transform.Find("name").GetComponent<UILabel>();
        effect_label = transform.Find("effet").GetComponent<UILabel>();
        pricesell_label = transform.Find("press_sell").GetComponent<UILabel>();
    }

    public void setid(int id)
    {
        this.id = id;
        info = objectsinfo._instance.GetObjectInFoById(id);

        Icon_sprite.spriteName = info.icon_name;
        name_label.text = info.name;
        if(info.attack>0)
        {
            effect_label.text = "+伤害 " + info.attack;
        }
        else if(info.def>0)
        {
            effect_label.text = "防御 " + info.def;
        }
        else if(info.speed>0)
        {
            effect_label.text = "速度 " + info.speed;
        }

        pricesell_label.text = info.price_sell.ToString();
    }

    public void OnbuyButton()
    {
        weaponshop._instance.OnbuyClick(id);
    }


}
