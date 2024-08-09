using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_chase_sky : MonoBehaviour
{
    [Header("추격 속도")]
    [SerializeField][Range(1f, 11f)] float chase_velocity = 6f;
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
        //if (Vector2.Distance(Transform.position, player_tr.position) > 20)  //안 움직일 경우 
        //{
        //    animator.SetBool("IsMove", false);  //애니메이터의 참거짓 값을 거짓으로
        //}
        //else { animator.SetBool("IsMove", true); }  //움직이는 경우 애니메이터의 참거짓 값을 참으로

        //추격
        chase();
        //플레이어와 x좌표를 비교해 누가 좌우에 있는지에 따라 바라보는 방향 바꾸기
        if (transform.position.x < player_tr.transform.position.x) 
            {  spriterenderer.flipX = right; } 
        if (transform.position.x > player_tr.transform.position.x)
            { spriterenderer .flipX = left; }

    }

    void chase()
    {
        if (Vector2.Distance(Transform.position, player_tr.position) < 20)  //플레이어에게까지의 거리를 계산, 거리가 20이내라면 그 위치로 서서히 다가감
            Transform.position = Vector2.MoveTowards(Transform.position, player_tr.position, chase_velocity * Time.deltaTime);  
                
        else
            transform.position = new Vector2(transform.position.x, transform.position.y); //멀리가면 그 자리에 있기
            
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    }

}
