using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public CircleCollider2D HeadHitbox;
    public EdgeCollider2D BodyHitbox_1;
    public EdgeCollider2D BodyHitbox_2;

    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pistol_Bullet"))
        {
            PistolBulletHit = true;
        }

        else if (collision.collider.CompareTag("Rifle_Bullet"))
        {
            RifleBulletHit = true;
        }

        else if (collision.collider.CompareTag("Knife"))
        {
            KnifeHit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pistol_Bullet"))
        {
            PistolBulletHit = false;
        }

        else if (collision.collider.CompareTag("Rifle_Bullet"))
        {
            RifleBulletHit = false;
        }

        else if (collision.collider.CompareTag("Knife"))
        {
            KnifeHit = false;
        }
    }
}
