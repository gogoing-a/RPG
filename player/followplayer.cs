using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{

    private Transform player;
    private Vector3 offsetposition;
    private bool isRotating = false;
    public float distance = 0;
    public float scrollSpeed = 10;
    public float rorateSpeed = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(tags.player).transform;
        transform.LookAt(player.position);
        offsetposition = transform.position-player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offsetposition + player.position;
        RotateView();
        ScrollView();
    }

    private void RotateView()
    {
        //Input.GetAxis("Mouse X");
        //Input.GetAxis("Mouse Y");

        if(Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        if(Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if(isRotating)
        {
            transform.RotateAround(player.position,player.up, rorateSpeed * Input.GetAxis("Mouse X"));

            Vector3 oringinalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -rorateSpeed * Input.GetAxis("Mouse Y"));

            float x = transform.eulerAngles.x;

            if(x<10||x>80)
            {
                transform.position = oringinalPos ;
                transform.rotation = originalRotation;
            }
        }

        offsetposition = transform.position - player.position;
    }

    private void ScrollView()
    {
        //print(Input.GetAxis("Mouse ScrollWheel"));//向前返回正值，向后返回负值（拉近视野）        
        distance = offsetposition.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 2, 18);
        offsetposition = offsetposition.normalized * distance;
    }
}
