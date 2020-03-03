using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControWalkState
{
    Moving,
    Idle
}

public class playermove : MonoBehaviour
{

    public float speed = 4;
    public ControWalkState state = ControWalkState.Idle;
    private playerdir dir;
    private playerattack attack;
    private CharacterController chara;
    public bool ismoving = false;
    // Start is called before the first frame update
    void Start()
    {
        dir = this.GetComponent<playerdir>();
        chara = this.GetComponent<CharacterController>();
        attack = this.GetComponent<playerattack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            float distance = Vector3.Distance(dir.targetposition, this.transform.position);

            if (distance > 0.3f)
            {
                ismoving = true;
                state = ControWalkState.Moving;
                chara.SimpleMove(transform.forward * speed);
            }
            else
            {
                ismoving = false;
                state = ControWalkState.Idle;
            }
        }
    }


    public void simpleMove(Vector3 targetpos)
    {
        transform.LookAt(targetpos);
        chara.SimpleMove(transform.forward*speed);    
    }
}
