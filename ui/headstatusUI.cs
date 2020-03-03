using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headstatusUI : MonoBehaviour
{

    public static headstatusUI _instance;

    private UILabel name;
    private UISlider hpbar;
    private UISlider mpbar;
    private UILabel hplabel;
    private UILabel mplabel;
    private playerstatus ps;

    private void Awake()
    {
        _instance = this;
        name = transform.Find("name").GetComponent<UILabel>();
        hpbar = transform.Find("Hp").GetComponent<UISlider>();
        mpbar= transform.Find("Mp").GetComponent<UISlider>();

        hplabel = transform.Find("Hp/Thumb/Label").GetComponent<UILabel>();
        mplabel = transform.Find("Mp/Thumb/Label").GetComponent<UILabel>();
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
        UpdateShow();
    }

    public void UpdateShow()
    {
        name.text = "LV." + ps.level + " " + ps.name;
        hpbar.value = ps.hp_remain/ps.hp;
        mpbar.value = ps.mp_remain / ps.mp;

        hplabel.text = ps.hp_remain + "/" + ps.hp;
        mplabel.text = ps.mp_remain + "/" + ps.mp;
    }
}
