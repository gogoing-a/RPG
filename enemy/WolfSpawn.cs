using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawn : MonoBehaviour
{
    public int maxnumber = 5;
    private int currentnum = 0;
    public float time = 3;
    private float timer = 0;
    public GameObject prefab;

    private void Update()
    {
        if(currentnum<maxnumber)
        {
            timer += Time.deltaTime;
            if(timer>time)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-5, 5);
                pos.z += Random.Range(-5, 5);
                GameObject go= GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
                go.GetComponent<wolfbaby>().spawn = this;
                timer = 0;
                currentnum++;
            }
        }
    }


    public void MinusNumer()
    {
        this.currentnum--;
    }
}
