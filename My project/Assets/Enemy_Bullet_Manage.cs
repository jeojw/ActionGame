using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Manage : MonoBehaviour
{
    int direct;
    Enemy_Movement EMove;
    SpriteRenderer BulletSprite;
    Rigidbody2D physics;

    GameObject EventSystem;

    float BulletSpeed = 120f;
    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        BulletSprite = GetComponent<SpriteRenderer>();
        EventSystem = GameObject.Find("EventSystem");
    }

    public void SetDirect(int _direct)
    {
        direct = _direct;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") ||
            collision.CompareTag("Slope") ||
            collision.CompareTag("Player"))
        {
            physics.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 Pos_Camera = Camera.main.WorldToViewportPoint(transform.position);
        if (Pos_Camera.x < 0f || Pos_Camera.x > 1f)
        {
            physics.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        if (direct == -1)
            BulletSprite.flipY = true;
        else
            BulletSprite.flipY = false;

        physics.velocity = new Vector2(direct * BulletSpeed, 0);
    }
}
