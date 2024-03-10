using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Manage : MonoBehaviour
{
    int direct;
    BoxCollider2D BoxC;
    Enemy_Movement EMove;
    SpriteRenderer BulletSprite;
    Rigidbody2D physics;

    GameObject EventSystem;

    float BulletSpeed = 120f;
    // Start is called before the first frame update
    void Start()
    {
        BoxC = GetComponent<BoxCollider2D>();
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
            Destroy(gameObject);
        }
    }

    private void CastColliderCheck()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(transform.position, BoxC.size * 0.02f, -90f, new Vector2((int)direct, 0), 0.02f, LayerMask.GetMask("Player"));
        RaycastHit2D Hit2 = Physics2D.BoxCast(transform.position, BoxC.size * 0.16f, -90f, new Vector2((int)direct, 0), 0.02f, LayerMask.GetMask("Slope"));
        RaycastHit2D Hit3 = Physics2D.BoxCast(transform.position, BoxC.size * 0.16f, -90f, new Vector2((int)direct, 0), 0.02f, LayerMask.GetMask("Ground"));
        if (Hit.collider != null || Hit2.collider != null || Hit3.collider != null)
            Destroy(gameObject);
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
        if (direct == -1)
            BulletSprite.flipY = true;
        else
            BulletSprite.flipY = false;

        physics.velocity = new Vector2(direct * BulletSpeed, 0);
    }
}
