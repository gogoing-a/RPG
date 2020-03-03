using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expbar : MonoBehaviour
{
    public static expbar _instance;

    private UISlider progressBar;
    private void Awake()
    {
        _instance = this;
        progressBar = this.GetComponent<UISlider>();
    }
     
    public void setvalue(float value)
    {
        progressBar.value = value;
    }
}
