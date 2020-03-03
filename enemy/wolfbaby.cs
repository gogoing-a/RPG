using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}

public class wolfbaby : MonoBehaviour
{
    public WolfState state = WolfState.Idle;

    public string animname_death;

    public string animname_idle;
    public string animname_walk;
    public string animname_now;
    public float time=1;
    public float timer = 0;
    public float speed=1;
    public int hp = 100;
    public int attack = 10;
    public int exp = 20;
    public float miss_rate = 0.2f;

    private bool isattacked = false;
    private Animation animations;
    private CharacterController cc;
    public Renderer normal;
    private Color nomals;
    public AudioClip miss_sound;

    private GameObject hudtextFollow;
    private GameObject hudtextGo;
    public GameObject hudtextprefab;

    private HUDText hudtext;
    private UIFollowTarget followtarget;

    public string aniname_normalattack;
    public float time_normalattack;
    public string aniname_crazyattack;
    public float time_crazyattack;

    public float crazyattack_rate;

    public string aniname_attack_now;
    public int attack_rate=1;
    private float attack_timer = 0;

    public Transform target;

    public float mindistance=2;
    public float maxdistance=5;

    public WolfSpawn spawn;

    private playerstatus ps;

    private void Awake()
    {
        animations = this.GetComponent<Animation>();
        animname_now = animname_idle;
        cc = this.GetComponent<CharacterController>();
        nomals = normal.material.color;
        hudtextFollow = transform.Find("HUDtext").gameObject;
    }

    private void Start()
    {
        //hudtextGo= GameObject.Instantiate(hudtextprefab, Vector3.zero, Quaternion.identity) as GameObject;
        //hudtextGo.transform.parent = HUDTextParent._instance.gameObject.transform;
        hudtextGo= NGUITools.AddChild(HUDTextParent._instance.gameObject, hudtextprefab);

        hudtext = hudtextGo.GetComponent<HUDText>();
        followtarget = hudtextGo.GetComponent<UIFollowTarget>();
        followtarget.target = hudtextFollow.transform;
        followtarget.gameCamera = Camera.main;
        //followtarget.uiCamera = UICamera.current.GetComponent<Camera>();
        //followtarget.uiCamera=transform.parent.FindChild("Camer").GetComponent<Camera>();
        ps = GameObject.FindGameObjectWithTag(tags.player).GetComponent<playerstatus>();
    }

    private void Update()
    {
        if(state==WolfState.Death)
        {
            animations.CrossFade(animname_death);
        }
        else if(state==WolfState.Attack)
        {
            AutoAttack();
        }
        else
        {
            animations.CrossFade(animname_now);
            if(animname_now==animname_walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }

            timer += Time.deltaTime;
            if(timer>=time)
            {
                timer = 0;
                RandomState();
            }
        }
    }

    

    private void RandomState()
    {
        int value = UnityEngine.Random.Range(0,2);
        if (value == 0)
        {
            animname_now = animname_idle;
        }
        else
        {
            if(animname_now!=animname_walk)
            {
                transform.Rotate(transform.up * UnityEngine.Random.Range(0, 360));
            }
            animname_now = animname_walk;
        }
    }

    public void TakeDamage(int attack)
    {
        if (state == WolfState.Death) return;
        target = GameObject.FindGameObjectWithTag(tags.player).transform;
        state = WolfState.Attack;
        float value = UnityEngine.Random.Range(0, 1f);
        if(value<miss_rate)
        {
            AudioSource.PlayClipAtPoint(miss_sound,transform.position);
            hudtext.Add("Miss", Color.gray, 1);
        }
        else
        {
            hudtext.Add("-"+attack, Color.red, 1);
            this.hp -= attack;
            StartCoroutine(showbodyRed());
            isattacked = true;
            attack_timer = 0;
            if(hp<=0)
            {
                state = WolfState.Death;
                Destroy(this.gameObject, 2);
            }
        }
    }

    IEnumerator showbodyRed()
    {
        normal.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        normal.material.color = nomals;
    }

    private void AutoAttack()
    {
        if(target!=null)
        {
            PlayerState plyerState = target.GetComponent<playerattack>().state;
            if(plyerState == PlayerState.Death)
            {
                target = null;
                state = WolfState.Idle;return;
            }
            float distance = Vector3.Distance(target.position, transform.position);
            if(distance>maxdistance)
            {
                target = null;
                state = WolfState.Idle;
            }
            else if(distance<=mindistance)
            {
                attack_timer += Time.deltaTime;
                animations.CrossFade(aniname_attack_now);
                if(aniname_attack_now==aniname_normalattack)
                {
                    if(attack_timer>time_normalattack)
                    {
                        target.GetComponent<playerattack>().TakeDamage(attack);
                        aniname_attack_now = animname_idle;
                    }
                }
                else if(aniname_attack_now==aniname_crazyattack)
                {
                    if (attack_timer > time_crazyattack)
                    {
                        target.GetComponent<playerattack>().TakeDamage(attack);
                        aniname_attack_now = animname_idle;
                    }
                }
                if(attack_timer>(1f/attack_rate))
                {
                    RandomAttack();
                    attack_timer = 0;
                }
            }
            else
            {
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                animations.CrossFade(animname_walk);
            }
        }
        else
        {
            state = WolfState.Idle;
        }
    }

    private void RandomAttack()
    {
        float value = UnityEngine.Random.Range(0f, 1f);
        if(value<crazyattack_rate)
        {
            aniname_attack_now = aniname_crazyattack;
        }
        else
        {
            aniname_attack_now = aniname_normalattack;
        }
    }

    private void OnDestroy() 
    {
        spawn.MinusNumer();
        ps.GetExp(exp);
        Barnpc._instance.OnKillWolf();
        GameObject.Destroy(hudtextGo);
    }

    private void OnMouseEnter()
    {
        if (playerattack._instance.isLockingTaret == false)
        cursoumanager._instance.SatAttack();
    }

    private void OnMouseExit()
    {
        if (playerattack._instance.isLockingTaret == false)
        cursoumanager._instance.SetNormal();
    }

}
