using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ground : MonoBehaviour
{
    [Header("플랫폼감지 보간계수(높을수록 디버깅 선이 멀리 감)")]    //몬스터마다 크기가 달라서 플랫폼감지선 개별조절
    [SerializeField][Range(0, 0.8f)] float parameter_for_lay = 0.2f;
    [Header("이동속도")]        //유니티 인스펙터 창에서 조절 가능(코드 상으로는 -1곱해서 뒤집는 거 말고는 안 건드림)
    [SerializeField][Range(-11f, 11f)] float gr_enemy_vel = 6f;
    // Start is called before the first frame update
    Rigidbody2D gr_rigid;
    SpriteRenderer gr_spriterenderer;
    Animator gr_animator;
    float float_for_lay;
    bool gr_right;
    bool gr_left;
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
        //이동로직을 위한 보간계수 조정
        float_for_lay = gr_rigid.position.x + gr_enemy_vel * parameter_for_lay;
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
        Vector2 frontVec = new Vector2(float_for_lay , gr_rigid.position.y); //보간계수적용하기

        Debug.DrawRay(frontVec, Vector3.down, Color.green); //잘 되고 있는지 확인하는 용도

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down , 1, LayerMask.GetMask("Platform")); //실제 적용부분
        if (rayHit.collider == null)
        {
            gr_enemy_vel = gr_enemy_vel *-1;  //속도를 반대방향으로 바꾸기
            if (gr_enemy_vel >0) { gr_spriterenderer.flipX = gr_right; } //양의 방향으로 움직일 경우 스프라이트 뒤집기
            else if (gr_enemy_vel <0) { gr_spriterenderer.flipX = gr_left; } //음의 방향으로 움직일 경우 스프라이트 원래 방향 쓰기
        }

    }


}
