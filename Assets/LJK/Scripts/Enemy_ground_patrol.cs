using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ground : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D gr_rigid;
    SpriteRenderer gr_spriterenderer;
    Animator gr_animator;
    bool gr_right;
    bool gr_left;
    public float gr_enemy_vel;  //유니티 인스펙터 창에서 조절 가능(코드 상으로는 -1곱해서 뒤집는 거 말고는 안 건드림)
    void Start()
    {
        if (gr_spriterenderer.flipX == true)  //기본적으로 왼쪽 바라보는 적들/ 오른쪽 바라보는 스프라이트 수정한 경우
        {
            gr_right = false;
            gr_left = true;
        }
        else
        {
            gr_right = true;
            gr_left = false;
        }
    }

    void Awake()
    {
        gr_spriterenderer = GetComponent<SpriteRenderer>();
        gr_rigid = GetComponent<Rigidbody2D>();
        gr_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //이동 상태에 따른 애니메이션 전환
        if (gr_enemy_vel== 0)  //안 움직일 경우 (x방향속도==0)
        {
            gr_animator.SetBool("IsMove", false);  //애니메이터의 참거짓 값을 거짓으로
        }
        else { gr_animator.SetBool("IsMove", true); }  //움직이는 경우 애니메이터의 참거짓 값을 참으로

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //이동속도
        gr_rigid.velocity = new Vector2(gr_enemy_vel, 0);

        //이동로직(안 떨어지면서 다니기)
        Vector2 frontVec = new Vector2(gr_rigid.position.x + gr_enemy_vel /2, gr_rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down , 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            gr_enemy_vel = gr_enemy_vel *-1;  //속도를 반대방향으로 바꾸기
            if (gr_enemy_vel >0) { gr_spriterenderer.flipX = gr_right; } //양의 방향으로 움직일 경우 스프라이트 뒤집기
            else if (gr_enemy_vel <0) { gr_spriterenderer.flipX = gr_left; } //음의 방향으로 움직일 경우 스프라이트 원래 방향 쓰기
        }

    }


}
