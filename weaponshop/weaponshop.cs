using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponshop : MonoBehaviour
{
    public static weaponshop _instance;
    public int[] weaponidArray;
    public UIGrid grid;
    public GameObject weaponitem;

    private TweenPosition tween;
    private bool isshow=false;
    private GameObject numberdialog;
    private UIInput numberinput;
    private int buyid = 0;

    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        numberdialog = transform.Find("Panel/numperdialog").gameObject;
        numberinput = transform.Find("Panel/numperdialog/numperinput").GetComponent<UIInput>();
        numberdialog.SetActive(false);
    }
    private void Start()
    {
        Initshopweapon();
    }

    public void Transfromstate()
    {
        if(isshow)
        {
            tween.PlayReverse();
            isshow = false;
        }
        else
        {
            tween.PlayForward();
            isshow = true;
        }
    }

    public void OncloseButtonClick()
    {
        Transfromstate();
    }

    void Initshopweapon()
    {
        foreach(int id in weaponidArray)
        {
            GameObject itemGo= NGUITools.AddChild(grid.gameObject, weaponitem);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<shopweaponitem>().setid(id);
        }
    } 

    public void OnOkBtnClick()
    {
        int count=int.Parse(numberinput.value);
        if(count>0)
        {
            int price = objectsinfo._instance.GetObjectInFoById(buyid).price_buy;
            int total_price = price * count;
            bool success = inventory._instance.GetCoin(total_price);
            if (success)
            {
                inventory._instance.GetId(buyid, count);
            }
        }
        buyid = 0;
        numberinput.value = "0";
        numberdialog.SetActive(false);
    }

    public void OnbuyClick(int id)
    {
        buyid = id;
        numberdialog.SetActive(true);
        numberinput.value = "0";
    }

    public void OnClick()
    {
        numberdialog.SetActive(false);
    }
}
