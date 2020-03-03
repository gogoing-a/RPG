using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillitemicon : UIDragDropItem
{
    private int skillid;

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();

        skillid = transform.parent.GetComponent<skillitem>().id;
        transform.parent = transform.root;
        this.GetComponent<UISprite>().depth = 40;
    } 

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        if(surface!=null&&surface.tag==tags.shortcut)
        {
            surface.GetComponent<shortcutgrid>().SetSkill(skillid);
        }
    }
}
