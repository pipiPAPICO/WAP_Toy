using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sky : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("�� ���� �̵��ð�")]        //����Ƽ �ν����� â���� ���� ����(�ڵ� �����δ� -1���ؼ� ������ �� ����� �� �ǵ帲)
    [SerializeField][Range(-3f, 3f)] float flying_time = 1f;
    [Header("�̵��ӵ�")]        //����Ƽ �ν����� â���� ���� ����(�ڵ� �����δ� -1���ؼ� ������ �� ����� �� �ǵ帲)
    [SerializeField][Range(-8f, 8f)] float sky_enemy_vel = -4f;
    Rigidbody2D sky_rigid;
    SpriteRenderer sky_spriterenderer;
    Animator sky_animator;
    bool sky_right;
    bool sky_left;
    public bool Check_if_want_vertical;   //inspector���� üũ�ϸ� ����(����)�� �ٴ� ��(üũ �� �ϸ� �¿�� �ٴ�)
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
        if (Check_if_want_vertical == true) //����, y������ �ٴ�
        {
            nvec = new Vector2(sky_rigid.velocity.x, sky_enemy_vel);
        }
        else //if (Check_if_want_vertical == false) // �¿�, x������ �ٴ�
        {
            nvec = new Vector2(sky_enemy_vel, sky_rigid.velocity.y);
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
        //�̵��ӵ�
        sky_rigid.velocity = nvec;

        

    }


}
