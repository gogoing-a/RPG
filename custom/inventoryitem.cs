using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryitem : UIDragDropItem 
{
    public UISprite sprite;
    private int id;

    void Awake()
    {
        base.Awake();       
        sprite = this.GetComponent<UISprite>();       
    }

    void Update()
    {
        base.Update();
        if(isHover)
        {
            //显示提示信息
            inventorydes._instance.show(id);

            if(Input.GetMouseButtonDown(1))//出现穿戴功能
            {
                
                bool success = EquipmentUI._instance.Dress(id);
                if(success)
                {                  
                    transform.parent.GetComponent<inventoryitemgrid>().MinusNumber();
                }
            }
        }
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.tag == tags.inventot_item_Grid)
            {
                if (surface == this.transform.parent.gameObject)
                {
                    //resetPoisition();
                }
                else
                {
                    inventoryitemgrid oldParent = this.transform.parent.GetComponent<inventoryitemgrid>();
                    this.transform.parent = surface.transform;
                    //resetPoisition();
                    inventoryitemgrid newParent = surface.GetComponent<inventoryitemgrid>();
                    newParent.selid(oldParent.id, oldParent.num);
                    oldParent.clearInfo();
                }
            }
            else if (surface.tag == tags.inventot_item)
            {
                inventoryitemgrid grid1 = this.transform.parent.GetComponent<inventoryitemgrid>();
                inventoryitemgrid grid2 = surface.transform.parent.GetComponent<inventoryitemgrid>();
                int id = grid1.id;int num = grid1.num;
                grid1.selid(grid2.id, grid2.num);
                grid2.selid(id, num);
            }
            else if(surface.tag==tags.shortcut)
            {
                surface.GetComponent<shortcutgrid>().SetInventory(id);
            }
        }
        resetPoisition();
    }

    void resetPoisition()
    {
        transform.localPosition = Vector3.zero;
    }

    public void SetId(int id)
    {
        objectinfo info = objectsinfo._instance.GetObjectInFoById(id);
        sprite.spriteName = info.icon_name;
    }

    public void setIconName(int id,string icon_name)
    {
        this.id = id;
        sprite.spriteName = icon_name;
    }
    private bool isHover = false;

    public void onHoverOver()
    {
        isHover = true;
    }

    public void onHoverOut()
    {
        isHover = false;
    }
}
