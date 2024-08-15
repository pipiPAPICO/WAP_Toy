using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_chase_ground : MonoBehaviour
{
    [Header("�߰� �ӵ�")]
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
        if (spriterenderer.flipX == true)  //�⺻������ ���� �ٶ󺸴� ����/ ������ �ٶ󺸴� ��������Ʈ ������ ���
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
        //�̵� ���¿� ���� �ִϸ��̼� ��ȯ
        if (Vector2.Distance(Transform.position, player_tr.position) > 15)  //�� �Ѿư��� ���ǰ� ����
        {
            animator.SetBool("IsMove", false);  //�ִϸ������� ������ ���� ��������
        }
        else { animator.SetBool("IsMove", true); }  //�����̴� ��� �ִϸ������� ������ ���� ������

        //�÷��̾�� x��ǥ�� ���� ���� �¿쿡 �ִ����� ���� �ٶ󺸴� ���� �ٲٱ�
        if (transform.position.x < player_tr.transform.position.x) 
            {  spriterenderer.flipX = right; } 
        if (transform.position.x > player_tr.transform.position.x)
            { spriterenderer .flipX = left; }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�÷��̾�� x��ǥ�� ���� ���� �¿쿡 �ִ����� ���� �Ѿư���
        if (Vector2.Distance(Transform.position, player_tr.position) < 15)  //�Ѿư� �� �ִ� ���� ���ΰ��
            if (transform.position.x < player_tr.transform.position.x)  //�÷��̾ �����ʿ� �ִ� ���
                 { rigid.velocity = new Vector2(chase_velocity,  rigid.velocity.y); }
        //if (Vector2.Distance(Transform.position, player_tr.position) < 15)
            //if (transform.position.x > player_tr.transform.position.x)  //�÷��̾ ���ʿ� �ִ� ���
             else    { rigid.velocity = new Vector2(-chase_velocity, rigid.velocity.y); }
    }

}
