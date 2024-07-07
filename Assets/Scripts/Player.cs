using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 inputVec;
    float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputVec.x = Input.GetAxis("Horizontal");
       if (Input.GetKey(KeyCode.A) == true) //a키를 입력했을 경우
        {
           Rigidbody.AddForce(Vector2.left *  speed); // 왼쪽으로 speed값만큼의 힘을 가한다
        }

        if (Input.GetKey(KeyCode.D) == true) //d키를 입력받았을 경우
        {
            Rigidbody.AddForce(Vector2.right * speed); // 오른쪽으로 speed값만큼의 힘을 가한다
        }
        if (Input.GetKeyUp(KeyCode.Space) == true) //스페이스바를 눌렀다 뗀 경우
        {
            //Rigidbody.AddForce(Vector2.up * speed);
            Rigidbody.velocity = Vector2.up * speed; //위 방향으로 속도가 생김
            //재입력(더블+점프)방지
        }

    }

    private void FixedUpdate()
    {
          // Rigidbody.AddForce(inputVec);
    }
}
