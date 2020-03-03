using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicSphere : MonoBehaviour
{
    public float attack = 0;
    private List<wolfbaby> wolfList = new List<wolfbaby>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == tags.enemy)
        {
            wolfbaby baby = other.GetComponent<wolfbaby>();
            int index = wolfList.IndexOf(baby);
            if(index==-1)
            {
                baby.TakeDamage((int)attack);
                wolfList.Add(baby);
            }
        }
    }
}
