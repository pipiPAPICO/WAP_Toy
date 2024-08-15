using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sky : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("한 방향 이동시간")]        //유니티 인스펙터 창에서 조절 가능(코드 상으로는 -1곱해서 뒤집는 거 말고는 안 건드림)
    [SerializeField][Range(-3f, 3f)] float flying_time = 1f;
    [Header("이동속도")]        //유니티 인스펙터 창에서 조절 가능(코드 상으로는 -1곱해서 뒤집는 거 말고는 안 건드림)
    [SerializeField][Range(-8f, 8f)] float sky_enemy_vel = -4f;
    Rigidbody2D sky_rigid;
    SpriteRenderer sky_spriterenderer;
    Animator sky_animator;
    int check_flip;
    bool sky_right;
    bool sky_left;
    public bool Check_if_want_vertical;   //inspector에서 체크하면 수직(상하)로 다닐 것(체크 안 하면 좌우로 다님)
    Vector2 nvec;
    void Start()
    {
    }

    void Awake()
    {
        sky_spriterenderer = GetComponent<SpriteRenderer>();
        sky_rigid = GetComponent<Rigidbody2D>();
        sky_animator = GetComponent<Animator>();
        Invoke("fly",flying_time);
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
            //속도가 바뀌기 전에 스프라이트 방향을 바꾸고 있으므로 이를 해소하기 위해 체크변수하나 쓰기
            if (check_flip == 0) { check_flip++; }
            else {
                if (sky_spriterenderer.flipX == false) { sky_spriterenderer.flipX = true; }
                else if (sky_spriterenderer.flipX == true) { sky_spriterenderer.flipX = false; }
            }
        }
            sky_enemy_vel = sky_enemy_vel * -1;
        Invoke("fly", flying_time);

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
