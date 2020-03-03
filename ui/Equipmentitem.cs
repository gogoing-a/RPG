using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipmentitem : MonoBehaviour
{
    public UISprite sprite;
    public int id;
    private bool isHover = false;

    private void Update()
    {
        if(isHover)
        {
            if(Input.GetMouseButtonDown(0))
            {
                EquipmentUI._instance.tankeoff(id,this.gameObject);
            }
        }
    }

    public void setid(int id)
    {
        this.id = id;
        objectinfo info = objectsinfo._instance.GetObjectInFoById(id);
        setinfo(info);
    }

    public void setinfo(objectinfo info)
    {
        this.id = info.id;
        sprite.spriteName = info.icon_name;
    }

    public void OnHover(bool isOver)
    {
        isHover = isOver;
    }
}
