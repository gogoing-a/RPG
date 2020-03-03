using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillitem : MonoBehaviour
{
    public int id;
    private SillInfo info;

    public UISprite icon_name_sprite;
    public UILabel name_label;
    public UILabel applytype_label;
    public UILabel des_label;
    public UILabel mp_label;

    private GameObject icon_mask;

    private void Awake()
    {
        icon_mask = transform.Find("icon_mask").gameObject;
        icon_mask.SetActive(false);
    }

    public void UpdateShow(int level)
    {
        if(info.level<=level)
        {
            icon_mask.SetActive(false);
            icon_name_sprite.GetComponent<skillitemicon>().enabled = true;
        }
        else
        {
            icon_mask.SetActive(true);
            icon_name_sprite.GetComponent<skillitemicon>().enabled = false;
        }        
    }
    
    public void Setld(int id)
    {
        this.id = id;
        info = skillsinfo._instance.GetSkillInfoById(id);
        icon_name_sprite.spriteName = info.icon_name;
        name_label.text = info.name;
        switch(info.applytype)
        {
            case ApplyType.Passive:
                applytype_label.text = "增益";
                break;
            case ApplyType.Buff:
                applytype_label.text = "增强";
                break;
            case ApplyType.SingleTarget:
                applytype_label.text = "单个目标";
                break;
            case ApplyType.MultiTarget:
                applytype_label.text = "群体技能";
                break;
        }
        des_label.text = info.des;
        mp_label.text = info.mp + "MP";
    }
}
