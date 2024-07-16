using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 inputVec;
    float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputVec.x = Input.GetAxis("Horizontal");
       if (Input.GetKey(KeyCode.A) == true) //aŰ�� �Է����� ���
        {
           Rigidbody.AddForce(Vector2.left *  speed); // �������� speed����ŭ�� ���� ���Ѵ�
        }

        if (Input.GetKey(KeyCode.D) == true) //dŰ�� �Է¹޾��� ���
        {
            Rigidbody.AddForce(Vector2.right * speed); // ���������� speed����ŭ�� ���� ���Ѵ�
        }
        if (Input.GetKeyUp(KeyCode.Space) == true) //�����̽��ٸ� ������ �� ���
        {
            //Rigidbody.AddForce(Vector2.up * speed);
            Rigidbody.velocity = 3 * speed * Vector2.up; //�� �������� �ӵ��� ����
            //���Է�(����+����)����
        }

    }

    private void FixedUpdate()
    {
          // Rigidbody.AddForce(inputVec);
    }
}
