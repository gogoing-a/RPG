using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryitemgrid : MonoBehaviour
{

    public int id = 0;
    public int num = 0;
    private objectinfo info = null;
    public UILabel numlabel;
    // Start is called before the first frame update
    void Start()
    {
        numlabel = this.GetComponentInChildren<UILabel>();
    }

    public void selid(int id,int num=1)
    {
        this.id = id; 
        info = objectsinfo._instance.GetObjectInFoById(id);
        inventoryitem item = this.GetComponentInChildren<inventoryitem>();
        item.setIconName(id,info.icon_name); 
        numlabel.enabled = true;
        this.num = num;
        numlabel.text = num.ToString();
    } 

    public void plusNumber(int num=1)
    {
        this.num += num;
        numlabel.text = this.num.ToString();
    }

    public bool MinusNumber(int num=1)
    {
        if(this.num>=num)
        {
            this.num -= num;
            numlabel.text = this.num.ToString();
            if (this.num==0)
            {
                clearInfo();
                GameObject.Destroy(this.GetComponentInChildren<inventoryitem>().gameObject);
            }
            return true;
        }
        return false;
    }

    public void clearInfo()
    {
        id = 0;
        info = null;
        num = 0;
        numlabel.enabled = false;
    }
}
