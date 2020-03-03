using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdir : MonoBehaviour
{

    public GameObject effect_click_prefab;
    private bool ismoving=false;
    public Vector3 targetposition = Vector3.zero;
    private playermove playerMove;
    private playerattack attack;

    // Start is called before the first frame update
    void Start()
    {
        targetposition = this.transform.position;
        playerMove = this.GetComponent<playermove>();
        attack = this.GetComponent<playerattack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerState.Death) return;
        if (attack.isLockingTaret==false&&Input.GetMouseButtonDown(0)&&UICamera.hoveredObject.name == "UI Root")
        {           
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool iscollider = Physics.Raycast(ray,out hitInfo);
            if(iscollider&&hitInfo.collider.tag==tags.ground)
            {
                ismoving = true;
                showclickeffect(hitInfo.point);
                lookattarget(hitInfo.point);
            }
        }

        if(Input.GetMouseButtonUp(0))
        { 
            ismoving = false;
        }

        if(ismoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool iscollider = Physics.Raycast(ray, out hitInfo);
            if (iscollider && hitInfo.collider.tag == tags.ground)
            {
                lookattarget(hitInfo.point);
            }
        }
        else
        {
            if(playerMove.ismoving)
            {
                lookattarget(targetposition);
            }
        }
    }

    void showclickeffect(Vector3 hitpoint)
    {
        hitpoint = new Vector3(hitpoint.x, hitpoint.y+0.1f, hitpoint.z);
        GameObject.Instantiate(effect_click_prefab, hitpoint, Quaternion.identity);
    }

    void lookattarget(Vector3 hitpoint)
    {
        targetposition = hitpoint;
        targetposition = new Vector3(targetposition.x, transform.position.y, targetposition.z);
        this.transform.LookAt(targetposition);
    }
}
