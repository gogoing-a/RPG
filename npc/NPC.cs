using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnMouseEnter()
    {
        cursoumanager._instance.SetNpcTalk();
    }

    private void OnMouseExit()
    {
        cursoumanager._instance.SetNormal();
    }
}
