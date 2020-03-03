using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playanimation : MonoBehaviour
{

    private playermove move;
    private Animation animation;

    private playerattack attack;
    // Start is called before the first frame update
    void Start()
    {
        move = this.GetComponent<playermove>();
        animation = this.GetComponent<Animation>();
        attack = this.GetComponent<playerattack>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            if (move.state == ControWalkState.Moving)
            {
                PlayAmi("Run");
            }
            else if (move.state == ControWalkState.Idle)
            {
                PlayAmi("Idle");
            }
        }
        else if(attack.state==PlayerState.NormalAttack)
        {
            if(attack.attack_state==AttackState.Moving)
            {
                PlayAmi("Run");
            }
        }
    }


    void PlayAmi(string AnimName)
    {
        animation.CrossFade(AnimName);
    }
}
