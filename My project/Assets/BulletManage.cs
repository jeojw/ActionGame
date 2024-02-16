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
    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        BulletSprite = GetComponent<SpriteRenderer>();
        BulletHitbox = GetComponent<BoxCollider2D>();
        if (transform.tag == "Pistol_Bullet")
            transform.position = GameObject.Find("PistolFirePosition").transform.position;
        else if (transform.tag == "Rifle_Bullet")
            transform.position = GameObject.Find("RifleFirePosition").transform.position;
        Player = GameObject.Find("Player");
        direct = Player.GetComponent<PlayerControl>().direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") ||
            collision.CompareTag("Slope") ||
            collision.CompareTag("Enemy") ||
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
