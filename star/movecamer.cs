using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecamer : MonoBehaviour
{

    public float speed = 10;

    private float enz = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.z<enz)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime,Space.World);
           
        }
    }
}
