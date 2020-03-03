using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopDragNpc : NPC
{
    private AudioSource aud;
    
    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
    }

    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))//弹出药品购买列表
        {
            aud.Play();
            shopdrag._instance.Transformstate();
        }
    }
}
