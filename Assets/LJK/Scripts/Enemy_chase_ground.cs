using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_chase_ground : MonoBehaviour
{
    [Header("추격 속도")]
    [SerializeField] [Range(1f, 11f)] float chase_velocity = 6f;
    // Start is called before the first frame update
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator animator;
    Transform Transform;
    Transform player_tr;
    bool right;
    bool left;
    //public int chase_velocity;
    void Start()
    {
        if (spriterenderer.flipX == true)  //기본적으로 왼쪽 바라보는 적들/ 오른쪽 바라보는 스프라이트 수정한 경우
        {
            right = false;
            left = true;
        }
        else
        {
            right = true;
            left = false;
        }
    }

    void Awake()
    {

        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Transform = GetComponent<Transform>();
        player_tr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();        
        
    }

    private void Update()
    {
        //이동 상태에 따른 애니메이션 전환
        if (Vector2.Distance(Transform.position, player_tr.position) > 20)  //안 움직일 경우 
        {
            animator.SetBool("IsMove", false);  //애니메이터의 참거짓 값을 거짓으로
        }
        else { animator.SetBool("IsMove", true); }  //움직이는 경우 애니메이터의 참거짓 값을 참으로

        //플레이어와 x좌표를 비교해 누가 좌우에 있는지에 따라 바라보는 방향 바꾸기
        if (transform.position.x < player_tr.transform.position.x) 
            {  spriterenderer.flipX = right; } 
        if (transform.position.x > player_tr.transform.position.x)
            { spriterenderer .flipX = left; }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //플레이어와 x좌표를 비교해 누가 좌우에 있는지에 따라 쫓아가기
        if (transform.position.x < player_tr.transform.position.x)  //플레이어가 오른쪽에 있는 경우
            if(chase_velocity < 0) { chase_velocity = chase_velocity * -1; } //속도가 왼쪽을 향하면 방향바꿈
        { rigid.velocity = new Vector2(chase_velocity, rigid.velocity.y); }
        if (transform.position.x > player_tr.transform.position.x)  //플레이어가 왼쪽에 있는 경우
            if (chase_velocity > 0) { chase_velocity = chase_velocity * -1; } //속도가 오른쪽을 향하면 방향바꿈
        { rigid.velocity = new Vector2( rigid.velocity.x, chase_velocity); }
    }

}
