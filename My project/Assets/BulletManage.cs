using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManage : MonoBehaviour
{
    Movement.Direction direct;
    public float BulletSpeed;
    public bool Cooldown;
    public SpriteRenderer BulletSprite;
    public BoxCollider2D BulletHitbox;
    public Rigidbody2D physics;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        BulletHitbox.enabled = false;
        BulletSprite.enabled = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Enemy"))
    //    {
    //        BulletSprite.enabled = false;
    //        BulletHitbox.enabled = false;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        BulletSprite.enabled = true;
        BulletHitbox.enabled = true;
        if (direct == Movement.Direction.LEFT)
        {
            physics.AddForce(transform.right * BulletSpeed * (-1), ForceMode2D.Force);
        }
        else
        {
            physics.AddForce(transform.right * BulletSpeed, ForceMode2D.Force);
        }

        //Cooldown = GameObject.Find("Player").GetComponent<WeaponManage>().Cooldown;
        //direct = GameObject.Find("Player").GetComponent<Movement>().direction;
        //if (Cooldown)
        //{
        //    BulletSprite.enabled = true;
        //    BulletHitbox.enabled = true;
        //    if (direct == Movement.Direction.LEFT)
        //    {
        //        physics.AddForce(transform.right * BulletSpeed * (-1), ForceMode2D.Force);
        //    }
        //    else
        //    {
        //        physics.AddForce(transform.right * BulletSpeed, ForceMode2D.Force);
        //    }
        //}
    }
}
