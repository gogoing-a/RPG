using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    private Camera minimapcamer;

    private void Start()
    {
        minimapcamer = GameObject.FindGameObjectWithTag(tags.minimap).GetComponent<Camera>();
    }

    public void Onzoominclick()
    {
        minimapcamer.orthographicSize--;
    }
    public void Onzoomoututclick()
    {
        minimapcamer.orthographicSize++;
    }
}
