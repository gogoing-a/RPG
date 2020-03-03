using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopdrag : MonoBehaviour
{

    public static shopdrag _instance;

    private TweenPosition tween;
    private bool isshow = false;
    private GameObject numberdialog;
    private UIInput numberInput;
    private int buy_id = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        numberdialog = this.transform.Find("numperdialog").gameObject;
        numberInput = transform.Find("numperdialog/numperinput").GetComponent<UIInput>();
        numberdialog.SetActive(false);
    }

    public void Transformstate()
    {
        if(isshow==false)
        {
            tween.PlayForward();
            isshow = true;
        }
        else
        {
            tween.PlayReverse();
            isshow = false;
        }
    }

    public void oncloseButtonClick()
    {
        Transformstate();
    }

    public void onbuyid1001()
    {
        buy(1001);
    }

    public void onbuyid1002()
    {
        buy(1002);
    }

    public void onbuyid1003()
    {
        buy(1003);
    }

    void buy(int id)
    {
        showNumberDialog();
        buy_id = id;
    }

    void showNumberDialog()
    {
        numberdialog.SetActive(true);
        numberInput.value = "0";
    }

    public void onokButtonclick()
    {
        int count = int.Parse(numberInput.value);
        objectinfo info = objectsinfo._instance.GetObjectInFoById(buy_id);
        int price = info.price_buy;
        int price_total = price * count;
        bool success = inventory._instance.GetCoin(price_total);

        if (success)
        {
            if (count > 0)
            {
                inventory._instance.GetId(buy_id,count);
            }
        }
        numberdialog.SetActive(false);
    }
}
