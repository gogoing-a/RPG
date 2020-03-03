using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functionBar : MonoBehaviour
{
    public void OnStatubuttonClick()
    {
        status._instace.TransformState();
    }

    public void OnBagButtonClick()
    {
        inventory._instance.TransformState();
    }

    public void OnEquipButtonClick()
    {
        EquipmentUI._instance.transformstate();
    }

    public void OnSkillButtonClick()
    {
        skillUI._instance.Transfromstate();
    }

    public void OnSettingButtonClick()
    {

    }
}
