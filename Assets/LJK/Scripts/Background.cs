using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer bg_sprite;
    Transform bg_transform;
    Transform player_tr;
    float screen_w = (float)Screen.width;
    float screen_h = (float)Screen.height;
    //float background_ratio = sprite.bounds.size.x / Sprite.bounds.size.y;

    // Start is called before the first frame update
    void Start()
    {
        bg_sprite = GetComponent<SpriteRenderer>();
        bg_transform = GetComponent<Transform>();
        player_tr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //화면비율만큼 기본 크기에 곱하기(다양한 화면 크기에서 완벽하진 않지만, 대체적으로 비슷하게 보임)
        bg_transform.localScale = new Vector2(bg_transform.localScale.x * (screen_w/screen_h), bg_transform.localScale.y *(screen_w/screen_h));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        bg_transform.position = new Vector2 (player_tr.transform.position.x + 4, 0);
    }
}
