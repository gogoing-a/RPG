using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SillAttak,
    Death
}

public enum AttackState
{
    Moving,
    Idle,
    Attack
}

public class playerattack : MonoBehaviour
{
    public static playerattack _instance;

    public PlayerState state = PlayerState.ControlWalk;

    public AttackState attack_state=AttackState.Idle;
    public string aniname_normalattack;
    public string aniname_idle;
    public string aniname_now;
    public float time_normalattack;
    public float rate_normalattack=1;
    private float timer = 0;
    public float min_distance=5;
    private Transform target_normalattack;
    private Animation animation;
    private playermove move;
    private bool showeffect = false;
    public GameObject effect;
    private playerstatus ps;
    public Renderer normal;
    private Color nomals;

    public float miss_rate = 0.25f;
    public GameObject hudtxtPrefab;
    private GameObject hudtxtFollow;
    private GameObject hudtextGo;
    private HUDText hudtext;
    public AudioClip miss_sound;
    public string aniname_death;

    public GameObject[] efxArray;
    private Dictionary<string, GameObject> efxdict = new Dictionary<string, GameObject>();

    public bool isLockingTaret = false;
    private SillInfo info = null;

    private void Awake()
    {
        _instance = this;
        move = this.GetComponent<playermove>();
        ps = this.GetComponent<playerstatus>();
        nomals = normal.material.color;
        hudtxtFollow = transform.Find("HUDtext").gameObject;

        foreach(GameObject go in efxArray)
        {
            efxdict.Add(go.name, go);
        }
    }

    private void Start()
    {
        animation = this.GetComponent<Animation>();

        hudtextGo = NGUITools.AddChild(HUDTextParent._instance.gameObject, hudtxtPrefab);

        hudtext = hudtextGo.GetComponent<HUDText>();
        UIFollowTarget followtarget = hudtextGo.GetComponent<UIFollowTarget>();
        followtarget.target = hudtxtFollow.transform;
        followtarget.gameCamera = Camera.main;
    }

    private void Update()
    {
        if (isLockingTaret == false&&Input.GetMouseButtonDown(0)&& state != PlayerState.Death)
        {
            //射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool iscollider = Physics.Raycast(ray, out hitInfo);
            if(iscollider&&hitInfo.collider.tag==tags.enemy)
            {
                target_normalattack = hitInfo.collider.transform;
                state = PlayerState.NormalAttack;
                timer = 0;
                showeffect = false;
            }
            else{
                state = PlayerState.ControlWalk;
                target_normalattack = null;
            }
        }

        if(state==PlayerState.NormalAttack)
        {
            if (target_normalattack == null)
            {
                state = PlayerState.ControlWalk;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target_normalattack.position);
                if (distance <= min_distance)
                {
                    attack_state = AttackState.Attack;
                    timer += Time.deltaTime;
                    animation.CrossFade(aniname_now);
                    if (timer >= time_normalattack)
                    {
                        aniname_now = aniname_idle;
                        if (showeffect == false)
                        {
                            showeffect = true;
                            GameObject.Instantiate(effect, target_normalattack.localPosition, Quaternion.identity);
                            target_normalattack.GetComponent<wolfbaby>().TakeDamage(GetAttack());
                        }
                    }
                    if (timer >= (1f / rate_normalattack))
                    {
                        timer = 0;
                        showeffect = false;
                        aniname_now = aniname_normalattack;
                    }
                    transform.LookAt(target_normalattack.position);
                }
                else
                {
                    attack_state = AttackState.Moving;
                    move.simpleMove(target_normalattack.position);
                }
            }
        }
        else if(state==PlayerState.Death)
        {
            animation.CrossFade(aniname_death);
        }
        if(Input.GetMouseButtonDown(0)&&isLockingTaret)
        {
            OnLockTarget();
        }
    }    

    public int GetAttack()
    {
        return (int)(EquipmentUI._instance.attack + ps.attack + ps.attack_plus);
    }

    public void TakeDamage(int attack) 
    {
        if (state == PlayerState.Death) return;
        float def = EquipmentUI._instance.def + ps.def + ps.def_plu;
        float temp = attack * ((200 - def) / 200);
        if (temp < 1) temp = 1;

        float value = UnityEngine.Random.Range(0f, 1f);
        if(value<miss_rate)
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);
            hudtext.Add("MISS", Color.gray, 1);
        }
        else
        {
            hudtext.Add("-" + temp, Color.red, 1);
            ps.hp_remain -= (int)temp;
            StartCoroutine(showbodyRed());
            if(ps.hp_remain<=0)
            {
                state = PlayerState.Death;
            }
        }
        headstatusUI._instance.UpdateShow();
    }

    IEnumerator showbodyRed()
    {
        normal.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        normal.material.color = nomals;
    }

    private void OnDestroy()
    {
        GameObject.Destroy(hudtextGo);
    }

    public void UseSkill(SillInfo info)
    {
        if(ps.heroType==HeroType.Megiclan)
        {
            if(info.applicableRole==ApplicableRole.Swordman)
            {
                return;
            }
        }
        if (ps.heroType == HeroType.Swordman)
        {
            if (info.applicableRole == ApplicableRole.Magician)
            {
                return;
            }
        }

        switch (info.applytype)
        {
            case ApplyType.Passive:
                StartCoroutine(OnpassiveSkillUse(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(OnbuffskillUse(info));
                break;
            case ApplyType.SingleTarget:
                Onsingletargetskilluse(info);
                break;
            case ApplyType.MultiTarget:
                OnMultiTargetSkillUse(info);
                break;
        }
    }


    IEnumerator OnpassiveSkillUse(SillInfo info)
    {
        state = PlayerState.SillAttak;
        animation.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;
        int hp = 0, mp = 0;
        if (info.applyProperty == ApplyProperty.HP)
        {
            hp = info.applyValue;
        }
        else if(info.applyProperty==ApplyProperty.MP)
        {
            mp = info.applyValue;
        }
        ps.GetDrug(hp,mp);
        GameObject prefab = null;
        efxdict.TryGetValue(info.efx_name,out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
    }

    IEnumerator OnbuffskillUse(SillInfo info)
    {
        state = PlayerState.SillAttak;
        animation.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;
        GameObject prefab = null;
        efxdict.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);

        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                ps.attack *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                ps.def *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                move.speed *= (info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack *= (info.applyValue / 100f);
                break;
        }

        yield return new WaitForSeconds(info.applyTime);

        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                ps.attack /= (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                ps.def /= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                move.speed /= (info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack /= (info.applyValue / 100f);
                break;
        }
    }

    void Onsingletargetskilluse(SillInfo info)
    {
        state = PlayerState.SillAttak;
        cursoumanager._instance.SetLockTarget();
        isLockingTaret = true;
        this.info = info;
    }

    private void OnLockTarget()
    {
        isLockingTaret = false;
        switch (info.applytype)
        {
            case ApplyType.SingleTarget:
                StartCoroutine(OnLockSingleTarget());
                break;
            case ApplyType.MultiTarget:
                StartCoroutine(OnLockMultiTarget());
                break;
        }
    }
 
    IEnumerator OnLockSingleTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool iscollider = Physics.Raycast(ray, out hitInfo);

        if(iscollider&&hitInfo.collider.tag==tags.enemy)
        {
            animation.CrossFade(info.aniname);
            yield return new WaitForSeconds(info.anitime);
            state = PlayerState.ControlWalk;

            GameObject prefab = null;
            efxdict.TryGetValue(info.efx_name, out prefab);
            GameObject.Instantiate(prefab, hitInfo.collider.transform.position, Quaternion.identity);

            hitInfo.collider.GetComponent<wolfbaby>().TakeDamage((int)(GetAttack() * (info.applyValue / 100f)));
        }
        else
        {
            state = PlayerState.NormalAttack;
        }
        cursoumanager._instance.SetNormal();
    }

    private void OnMultiTargetSkillUse(SillInfo info)
    {
        state = PlayerState.SillAttak;
        cursoumanager._instance.SetLockTarget();
        isLockingTaret = true;
        this.info = info;
    }

    IEnumerator OnLockMultiTarget()
    {
        cursoumanager._instance.SetNormal();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo; 
        bool iscollider = Physics.Raycast(ray, out hitInfo,13);

        if(iscollider)
        {
            animation.CrossFade(info.aniname);
            yield return new WaitForSeconds(info.anitime);
            state = PlayerState.ControlWalk;

            GameObject prefab = null;
            efxdict.TryGetValue(info.efx_name, out prefab);
            GameObject go= GameObject.Instantiate(prefab, hitInfo.point+Vector3.up*0.5f, Quaternion.identity);
            go.GetComponent<magicSphere>().attack = GetAttack() * (info.applyValue / 100f);
        }
        else
        {
            state = PlayerState.ControlWalk;
        }
    }

}