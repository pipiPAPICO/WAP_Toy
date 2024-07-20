using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_dino : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator animator;
    bool right;
    bool left;
    public int NextMove;
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
        Invoke("Think", 1); //애니메이션 적용 확인용으로 늦추기/디버깅 끝나면 그냥 Think() bear는 idle 애니메이션이 없는데 어카지
    }

    private void Update()
    {
        //이동 상태에 따른 애니메이션 전환
        if (NextMove == 0)  //안 움직일 경우 (x방향속도==0)
        {
            animator.SetBool("IsMove", false);  //애니메이터의 참거짓 값을 거짓으로
        }
        else { animator.SetBool("IsMove", true); }  //움직이는 경우 애니메이터의 참거짓 값을 참으로
        if (NextMove == 1) { spriterenderer.flipX = right; } //양의 방향으로 움직일 경우 스프라이트 뒤집기
        if (NextMove ==-1) { spriterenderer.flipX = left;} //음의 방향으로 움직일 경우 스프라이트 원래 방향 쓰기
        
        //방향전환  - player x 따오는 방법 공부하기
        //if (transform.position.x < Player_square.transform.position.x)     {  spriterenderer.flipX = true;   } 

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(NextMove*2, rigid.velocity.y);
    }

    void Think()    //몬스터가 움직임을 생각하는 함수
    {
        NextMove = Random.Range(-1, 2); //다음에 갈 x의 속도를 -1이상 2미만에서 정함
        Invoke("Think", 2); //2초 후에 이 함수를 다시 실행함(과부하 방지)
    }
}
