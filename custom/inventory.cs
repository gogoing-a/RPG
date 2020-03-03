using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{

    public static inventory _instance;
    public List<inventoryitemgrid> itemGridList = new List<inventoryitemgrid>();
    public UILabel coinNumberLabel;
    public GameObject inventoryItem;

    private TweenPosition tween;
    private int coincount = 1000;
    private void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(2001,2023));
        }       
    }
    public void GetId(int id,int count=1)
    {
        //第一步 查找所有物品中是否存在
        //第二步 如果存在，让物品加一
        //第三 如果不存在  查找空的方格
        inventoryitemgrid grid = null;
        foreach (inventoryitemgrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp; break;
            }
        }

        if (grid!=null)
        {
            grid.plusNumber(count);
        }
        else
        {            
            foreach (inventoryitemgrid temp in itemGridList)
            {
                if (temp.id == 0)
                {
                    grid = temp; break;
                }
            }
            if(grid!=null)
            {
                GameObject itemGo= NGUITools.AddChild(grid.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                grid.selid(id,count); 
            }
        }
    }

    public bool MinusId(int id,int count=1)
    {
        inventoryitemgrid grid = null;
        foreach (inventoryitemgrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp; break;
            }
        }

        if(grid==null)
        {
            return false;
        }
        else
        {
            bool isSucccess = grid.MinusNumber(count);
            return isSucccess;
        }
    }

    private bool isshow = false;

    void show()
    {
        isshow = true;
        tween.PlayForward();
    }

    void hide()
    {
        isshow = false;
        tween.PlayReverse();
    }

    public void TransformState()
    {
        if(isshow==false)
        {
            show();
        }
        else
        {
            hide();
        }
    }

    public void AddCoint(int count)
    {
        coincount += count;
        coinNumberLabel.text = coincount.ToString();
    }
    
    //这个是提款方法
    public bool GetCoin(int count)
    {
        if(coincount>=count)
        {
            coincount -= count;
            coinNumberLabel.text = coincount.ToString();
            return true;
        }
        return false;
    }
}
