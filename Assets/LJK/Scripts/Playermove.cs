using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class Playermove : MonoBehaviour
{
    [Header("플랫폼감지 보간계수(높을수록 디버깅 선이 멀리 감)")]    //플랫폼감지선 하나만 있으면 좌우 끝부분으로 착지할 때 점프를 더 못하는 버그 해결위함
    [SerializeField][Range(0, 0.8f)] float parameter_for_lay = 0.39f; //기본값0.39(0.4는 안 올라가져도 감지가 되고 0.3은 버그가 완전히 고쳐지지 않음)
    public float maxSpeed;
    public float jumpPower;
    float float_for_frontlay;
    float float_for_backlay;
    public int stagelevel;  //몇 스테이지인지 플레이어한테 기록해놓기(유니티 inspector창 이용)
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float_for_frontlay = rigid.position.x + parameter_for_lay;
        float_for_backlay  = rigid.position.x - parameter_for_lay;
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        //Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,rigid.velocity.y);

        }
        //방향 전환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw ("Horizontal") == -1;

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isrunning", false);
        else
            anim.SetBool("isrunning", true);

        //낙사한 경우
        if (rigid.transform.position.y < -5)
        {
            deathwarp(rigid.position.x);
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        if(rigid.velocity.y < 0)    //낙하 시 플랫폼 감지하여 점프상태 해제
        {
            //플랫폼에서 점프안하고 떨어지는동안 점프방지
            anim.SetBool("isJumping", true);
            //시작하는 지점을 앞뒤에 설정
            Vector2 frontVec = new Vector2(float_for_frontlay, rigid.position.y);
            Vector2 backVec  = new Vector2(float_for_backlay,  rigid.position.y);

            //확인용
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            Debug.DrawRay(backVec,  Vector3.down, new Color(0, 1, 0));
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            //앞에서 아래로
            RaycastHit2D FrayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (FrayHit.collider != null)
            {
                if (FrayHit.distance < 1f)
                    anim.SetBool("isJumping", false);
            }

            //뒤에서 아래로
            RaycastHit2D BrayHit = Physics2D.Raycast(backVec,  Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (BrayHit.collider != null)
            {
                if (BrayHit.distance < 1f)
                anim.SetBool("isJumping", false);
            }

            //중앙에서 아래로
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                    anim.SetBool("isJumping", false);
            }

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            deathwarp(rigid.position.x);
        }
        if (collision.gameObject.tag == "Finish")
        {
            Nextstage();
        }
        
    }

    public void deathwarp(float playerx) //(죽은 상황에서) 근처 리스폰 포인트로 워프시키기
    {
        if (stagelevel == 0) { SceneManager.LoadScene(1); } //버그나면 다음 스테이지를 가시면 됩니다
        if (stagelevel == 1)
        {
            if (playerx < 27)       { rigid.position = new Vector2(0, 3);  rigid.velocity = Vector2.zero; }
            else if (playerx < 56)  { rigid.position = new Vector2(19, 2); rigid.velocity = Vector2.zero; }
            else if (playerx < 200) { rigid.position = new Vector2(57, 2); rigid.velocity = Vector2.zero; } //끝까지
            else { SceneManager.LoadScene(stagelevel + 1); } //버그난 경우 초기화
        }
        if (stagelevel == 2) 
        {
            if (playerx < 26)       { rigid.position = new Vector2(-2, 2); rigid.velocity = Vector2.zero; }
            else if (playerx < 36)  { rigid.position = new Vector2(25, 5); rigid.velocity = Vector2.zero; }
            else if (playerx < 55)  { rigid.position = new Vector2(36, 5); rigid.velocity = Vector2.zero; }
            else if (playerx < 200) { rigid.position = new Vector2(54, 4); rigid.velocity = Vector2.zero; } //끝까지
            else { SceneManager.LoadScene(stagelevel); } //버그난 경우 초기화
        }
    }
    public void Nextstage()
    {
        //stagelevel++;
        if ((stagelevel < 2)) //스테이지가 3개인 상황(0,1,2)에서 마지막 스테이지가 아닐 경우
        {
            SceneManager.LoadScene(stagelevel+1);
        }
        else //마지막 스테이지일 경우
        {
            SceneManager.LoadScene(0);
        }


    }
}
