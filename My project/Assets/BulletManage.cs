using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManage : MonoBehaviour
{
    Movement.Direction direct;
    SpriteRenderer BulletSprite;
    BoxCollider2D BulletHitbox;
    Rigidbody2D physics;

    GameObject Player;
    public float BulletSpeed;
    public float Damage;
    public bool isCollision = false;
    public bool isOutCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        BulletSprite = GetComponent<SpriteRenderer>();
        BulletHitbox = GetComponent<BoxCollider2D>();
        transform.position = GameObject.Find("PistolFirePosition").transform.position;
        Player = GameObject.Find("Player");
        direct = Player.GetComponent<Movement>().direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("Slope"))
        {
            isCollision = true;
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 Pos_Camera = Camera.main.WorldToViewportPoint(transform.position);
        if (Pos_Camera.x < 0f || Pos_Camera.x > 1f)
        {
            isOutCamera = true;
            Destroy(gameObject);
        }
            

        float x = Input.GetAxisRaw("Horizontal");
        if (direct == Movement.Direction.LEFT)
        {
            BulletSprite.flipY = true;
            physics.velocity = new Vector2(Vector2.left.x * BulletSpeed, 0);
        }
        else
        {
            physics.velocity = new Vector2(Vector2.right.x * BulletSpeed, 0);
        }
        
    }
}
