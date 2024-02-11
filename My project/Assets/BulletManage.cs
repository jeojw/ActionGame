using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManage : MonoBehaviour
{
    PlayerControl.Direction direct;
    SpriteRenderer BulletSprite;
    BoxCollider2D BulletHitbox;
    Rigidbody2D physics;

    GameObject Player;
    public float BulletSpeed;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        BulletSprite = GetComponent<SpriteRenderer>();
        BulletHitbox = GetComponent<BoxCollider2D>();
        transform.position = GameObject.Find("PistolFirePosition").transform.position;
        Player = GameObject.Find("Player");
        direct = Player.GetComponent<PlayerControl>().direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("Slope") ||
            collision.collider.CompareTag("Enemy") ||
            collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 Pos_Camera = Camera.main.WorldToViewportPoint(transform.position);
        if (Pos_Camera.x < 0f || Pos_Camera.x > 1f)
        {
            Destroy(gameObject);
        }
            

        float x = Input.GetAxisRaw("Horizontal");
        if (direct == PlayerControl.Direction.LEFT)
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
