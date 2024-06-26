using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBulletManage : MonoBehaviour
{
    BoxCollider2D BoxC;
    PlayerControl.Direction direct;
    SpriteRenderer BulletSprite;
    Rigidbody2D physics;
    GameObject Player;
    float BulletSpeed = 120f;
    // Start is called before the first frame update
    void Start()
    {
        BoxC = GetComponent<BoxCollider2D>();   
        physics = GetComponent<Rigidbody2D>();
        BulletSprite = GetComponent<SpriteRenderer>();
        if (transform.tag == "Pistol_Bullet")
            transform.position = GameObject.Find("PistolFirePosition").transform.position;
        else if (transform.tag == "Rifle_Bullet")
            transform.position = GameObject.Find("RifleFirePosition").transform.position;
        Player = GameObject.Find("Player");
        direct = Player.GetComponent<PlayerControl>().direction;
    }

    private void CastColliderCheck()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(transform.position, BoxC.size * 0.24f, -90f, new Vector2((int)direct, 0), 0.02f, LayerMask.GetMask("Enemy"));
        RaycastHit2D Hit2 = Physics2D.BoxCast(transform.position, BoxC.size * 0.24f, -90f, new Vector2((int)direct, 0), 0.02f, LayerMask.GetMask("Slope"));
        RaycastHit2D Hit3 = Physics2D.BoxCast(transform.position, BoxC.size * 0.24f, -90f, new Vector2((int)direct, 0), 0.02f, LayerMask.GetMask("Ground"));
        if (Hit.collider != null || Hit2.collider != null || Hit3.collider != null)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") ||
            collision.CompareTag("Slope") ||
            collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CastColliderCheck();

        Vector3 Pos_Camera = Camera.main.WorldToViewportPoint(transform.position);
        if (Pos_Camera.x < 0f || Pos_Camera.x > 1f)
        {
            physics.velocity = Vector2.zero;
            Destroy(gameObject);
        }
            
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
