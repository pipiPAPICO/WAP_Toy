using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_chase : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator animator;
    Transform Transform;
    Transform player_tr;
    bool right;
    bool left;
    public int NextMove;
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
        player_tr = GetComponent<Transform>
        Invoke("Think", 0); //�ִϸ��̼� ���� Ȯ�ο����� ���߱�/����� ������ �׳� Think() bear�� idle �ִϸ��̼��� ���µ� ��ī��
    }

    private void Update()
    {
        //�̵� ���¿� ���� �ִϸ��̼� ��ȯ
        if (NextMove == 0)  //�� ������ ��� (x����ӵ�==0)
        {
            animator.SetBool("IsMove", false);  //�ִϸ������� ������ ���� ��������
        }
        else { animator.SetBool("IsMove", true); }  //�����̴� ��� �ִϸ������� ������ ���� ������
        if (NextMove == 1) { spriterenderer.flipX = right; } //���� �������� ������ ��� ��������Ʈ ������
        if (NextMove ==-1) { spriterenderer.flipX = left;} //���� �������� ������ ��� ��������Ʈ ���� ���� ����
        
        //������ȯ  - player x ������ ��� �����ϱ�
        //if (transform.position.x < Player_square.transform.position.x)     {  spriterenderer.flipX = true;   } 

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(NextMove*2, rigid.velocity.y);
    }

    void Think()    //���Ͱ� �������� �����ϴ� �Լ�
    {
        NextMove = Random.Range(-1, 2); //������ �� x�� �ӵ��� -1�̻� 2�̸����� ����
        Invoke("Think", 2); //2�� �Ŀ� �� �Լ��� �ٽ� ������(������ ����)
    }
}
