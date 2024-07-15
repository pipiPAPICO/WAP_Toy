using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;


public class Playermove : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 10;
    public float jumpPower = 10;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-5, 5);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(5, 5);

        }
       if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    bool IsGrounded()
    {
        // ĳ���͸� �߽����� �Ʒ� �������� ray �� ��� �� ���� Layer Ÿ���� Ground �� ��ü�� �ִ��� �˻��մϴ�.
        var ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
        return ray.collider != null;
    }
}
