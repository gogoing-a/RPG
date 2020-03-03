using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum shortcuttype
{
    Skill,
    Drug,
    None
}

public class shortcutgrid : MonoBehaviour
{

    public KeyCode keycode;

    private shortcuttype type = shortcuttype.None;
    private UISprite icon;
    private int id;
    private SillInfo skillinfo;
    private objectinfo objectinfo;
    private playerstatus ps;
    private playerattack pa;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
        pa = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerattack>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            if (type == shortcuttype.Drug)
            {
                OndrugUse();
            }
            else if(type==shortcuttype.Skill)
            {
                bool success = ps.TakeMP(skillinfo.mp);
                if(success==false)
                {

                }
                else
                {
                    pa.UseSkill(skillinfo);
                }
            }
        }
    }

    public void SetSkill(int id)
    {
        this.id = id;
        this.skillinfo = skillsinfo._instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = skillinfo.icon_name;
        type = shortcuttype.Skill;
    }

    public void SetInventory(int id)
    {
        this.id = id;
        objectinfo = objectsinfo._instance.GetObjectInFoById(id);
        if (objectinfo.type == objectType.Drug)
        {
            icon.gameObject.SetActive(true);
            icon.spriteName = objectinfo.icon_name;
            type = shortcuttype.Drug;
        }
    }
    public void OndrugUse()
    {
        bool success = inventory._instance.MinusId(id, 1);
        if (success)
        {
            ps.GetDrug(objectinfo.hp, objectinfo.mp);
        }
        else
        {
            type = shortcuttype.None;
            icon.gameObject.SetActive(false);
            id = 0;
            skillinfo = null;
            objectinfo = null;
        }
    }
}
