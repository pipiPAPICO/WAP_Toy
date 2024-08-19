using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class Playermove : MonoBehaviour
{
    [Header("�÷������� �������(�������� ����� ���� �ָ� ��)")]    //�÷��������� �ϳ��� ������ �¿� ���κ����� ������ �� ������ �� ���ϴ� ���� �ذ�����
    [SerializeField][Range(0, 0.8f)] float parameter_for_lay = 0.39f; //�⺻��0.39(0.4�� �� �ö����� ������ �ǰ� 0.3�� ���װ� ������ �������� ����)
    public float maxSpeed;
    public float jumpPower;
    float float_for_frontlay;
    float float_for_backlay;
    public int stagelevel;  //�� ������������ �÷��̾����� ����س���(����Ƽ inspectorâ �̿�)
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
        //���� ��ȯ
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw ("Horizontal") == -1;

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isrunning", false);
        else
            anim.SetBool("isrunning", true);

        //������ ���
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

        if(rigid.velocity.y < 0)    //���� �� �÷��� �����Ͽ� �������� ����
        {
            //�÷������� �������ϰ� �������µ��� ��������
            anim.SetBool("isJumping", true);
            //�����ϴ� ������ �յڿ� ����
            Vector2 frontVec = new Vector2(float_for_frontlay, rigid.position.y);
            Vector2 backVec  = new Vector2(float_for_backlay,  rigid.position.y);

            //Ȯ�ο�
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            Debug.DrawRay(backVec,  Vector3.down, new Color(0, 1, 0));
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            //�տ��� �Ʒ���
            RaycastHit2D FrayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (FrayHit.collider != null)
            {
                if (FrayHit.distance < 1f)
                    anim.SetBool("isJumping", false);
            }

            //�ڿ��� �Ʒ���
            RaycastHit2D BrayHit = Physics2D.Raycast(backVec,  Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (BrayHit.collider != null)
            {
                if (BrayHit.distance < 1f)
                anim.SetBool("isJumping", false);
            }

            //�߾ӿ��� �Ʒ���
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

    public void deathwarp(float playerx) //(���� ��Ȳ����) ��ó ������ ����Ʈ�� ������Ű��
    {
        if (stagelevel == 0) { SceneManager.LoadScene(1); } //���׳��� ���� ���������� ���ø� �˴ϴ�
        if (stagelevel == 1)
        {
            if (playerx < 27)       { rigid.position = new Vector2(0, 3);  rigid.velocity = Vector2.zero; }
            else if (playerx < 56)  { rigid.position = new Vector2(19, 2); rigid.velocity = Vector2.zero; }
            else if (playerx < 200) { rigid.position = new Vector2(57, 2); rigid.velocity = Vector2.zero; } //������
            else { SceneManager.LoadScene(stagelevel + 1); } //���׳� ��� �ʱ�ȭ
        }
        if (stagelevel == 2) 
        {
            if (playerx < 26)       { rigid.position = new Vector2(-2, 2); rigid.velocity = Vector2.zero; }
            else if (playerx < 36)  { rigid.position = new Vector2(25, 5); rigid.velocity = Vector2.zero; }
            else if (playerx < 55)  { rigid.position = new Vector2(36, 5); rigid.velocity = Vector2.zero; }
            else if (playerx < 200) { rigid.position = new Vector2(54, 4); rigid.velocity = Vector2.zero; } //������
            else { SceneManager.LoadScene(stagelevel); } //���׳� ��� �ʱ�ȭ
        }
    }
    public void Nextstage()
    {
        //stagelevel++;
        if ((stagelevel < 2)) //���������� 3���� ��Ȳ(0,1,2)���� ������ ���������� �ƴ� ���
        {
            SceneManager.LoadScene(stagelevel+1);
        }
        else //������ ���������� ���
        {
            SceneManager.LoadScene(0);
        }


    }
}
