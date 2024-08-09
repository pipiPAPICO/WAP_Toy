using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_chase_sky : MonoBehaviour
{
    [Header("�߰� �ӵ�")]
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
        //if (Vector2.Distance(Transform.position, player_tr.position) > 20)  //�� ������ ��� 
        //{
        //    animator.SetBool("IsMove", false);  //�ִϸ������� ������ ���� ��������
        //}
        //else { animator.SetBool("IsMove", true); }  //�����̴� ��� �ִϸ������� ������ ���� ������

        //�߰�
        chase();
        //�÷��̾�� x��ǥ�� ���� ���� �¿쿡 �ִ����� ���� �ٶ󺸴� ���� �ٲٱ�
        if (transform.position.x < player_tr.transform.position.x) 
            {  spriterenderer.flipX = right; } 
        if (transform.position.x > player_tr.transform.position.x)
            { spriterenderer .flipX = left; }

    }

    void chase()
    {
        if (Vector2.Distance(Transform.position, player_tr.position) < 20)  //�÷��̾�Ա����� �Ÿ��� ���, �Ÿ��� 20�̳���� �� ��ġ�� ������ �ٰ���
            Transform.position = Vector2.MoveTowards(Transform.position, player_tr.position, chase_velocity * Time.deltaTime);  
                
        else
            transform.position = new Vector2(transform.position.x, transform.position.y); //�ָ����� �� �ڸ��� �ֱ�
            
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    }

}
