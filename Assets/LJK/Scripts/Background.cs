using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer bg_sprite;
    Transform bg_transform;
    Transform player_tr;
    float screen = (float)Screen.width / Screen.height;
    //float background_ratio = sprite.bounds.size.x / Sprite.bounds.size.y;

    // Start is called before the first frame update
    void Start()
    {
        bg_sprite = GetComponent<SpriteRenderer>();
        bg_transform = GetComponent<Transform>();
        player_tr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        bg_transform.position = new Vector2 (player_tr.transform.position.x + 7, 0);
    }
}
