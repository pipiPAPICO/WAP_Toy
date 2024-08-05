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
        sky_rigid.velocity = new Vector2(0, sky_enemy_vel);
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
        sky_rigid.velocity = new Vector2(sky_rigid.velocity.x, sky_enemy_vel);

    }


}
