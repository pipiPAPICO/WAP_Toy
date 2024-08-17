using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ground : MonoBehaviour
{
    [Header("�÷������� �������(�������� ����� ���� �ָ� ��)")]    //���͸��� ũ�Ⱑ �޶� �÷��������� ��������
    [SerializeField][Range(0, 0.8f)] float parameter_for_lay = 0.2f;
    [Header("�̵��ӵ�")]        //����Ƽ �ν����� â���� ���� ����(�ڵ� �����δ� -1���ؼ� ������ �� ����� �� �ǵ帲)
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
        if (gr_spriterenderer.flipX == true)  //�⺻������ ���� �ٶ󺸴� ����/ ������ �ٶ󺸴� ��������Ʈ ������ ���
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
        //�̵������� ���� ������� ����
        float_for_lay = gr_rigid.position.x + gr_enemy_vel * parameter_for_lay;
        //�̵� ���¿� ���� �ִϸ��̼� ��ȯ
        if (gr_enemy_vel== 0)  //�� ������ ��� (x����ӵ�==0)
        {
            gr_animator.SetBool("IsMove", false);  //�ִϸ������� ������ ���� ��������
        }
        else { gr_animator.SetBool("IsMove", true); }  //�����̴� ��� �ִϸ������� ������ ���� ������

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //�̵��ӵ�
        gr_rigid.velocity = new Vector2(gr_enemy_vel, 0);

        //�̵�����(�� �������鼭 �ٴϱ�)
        Vector2 frontVec = new Vector2(float_for_lay , gr_rigid.position.y); //������������ϱ�

        Debug.DrawRay(frontVec, Vector3.down, Color.green); //�� �ǰ� �ִ��� Ȯ���ϴ� �뵵

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down , 1, LayerMask.GetMask("Platform")); //���� ����κ�
        if (rayHit.collider == null)
        {
            gr_enemy_vel = gr_enemy_vel *-1;  //�ӵ��� �ݴ�������� �ٲٱ�
            if (gr_enemy_vel >0) { gr_spriterenderer.flipX = gr_right; } //���� �������� ������ ��� ��������Ʈ ������
            else if (gr_enemy_vel <0) { gr_spriterenderer.flipX = gr_left; } //���� �������� ������ ��� ��������Ʈ ���� ���� ����
        }

    }


}
