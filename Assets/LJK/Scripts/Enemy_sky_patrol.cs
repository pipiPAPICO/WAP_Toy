using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sky : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D sky_rigid;
    SpriteRenderer sky_spriterenderer;
    Animator sky_animator;
    bool sky_right;
    bool sky_left;
    float sky_enemy_vel;
    public bool Check_if_want_vertical;   //inspector에서 체크하면 수직(상하)로 다닐 것(체크 안 하면 좌우로 다님)
    Vector2 nvec;
    void Start()
    {
    }

    void Awake()
    {
        sky_enemy_vel = -4; //가독성외에 필요없는 변수
        sky_spriterenderer = GetComponent<SpriteRenderer>();
        sky_rigid = GetComponent<Rigidbody2D>();
        sky_animator = GetComponent<Animator>();
        Invoke("fly",1);
    }

    void fly()
    {
        if (Check_if_want_vertical == true) //상하, y축으로 다님
        {
            nvec = new Vector2(sky_rigid.velocity.x, sky_enemy_vel);
        }
        else //if (Check_if_want_vertical == false) // 좌우, x축으로 다님
        {
            nvec = new Vector2(sky_enemy_vel, sky_rigid.velocity.y);
        }
            sky_enemy_vel = sky_enemy_vel * -1;
        Invoke("fly",1);

    }

    private void Update()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //이동속도
        sky_rigid.velocity = nvec;

        

    }


}
