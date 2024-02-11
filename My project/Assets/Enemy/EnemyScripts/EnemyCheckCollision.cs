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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pistol_Bullet"))
        {
            PistolBulletHit = true;
        }

        else if (collision.CompareTag("Rifle_Bullet"))
        {
            RifleBulletHit = true;
        }

        else if (collision.CompareTag("Knife"))
        {
            KnifeHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pistol_Bullet"))
        {
            PistolBulletHit = false;
        }

        else if (collision.CompareTag("Rifle_Bullet"))
        {
            RifleBulletHit = false;
        }

        else if (collision.CompareTag("Knife"))
        {
            KnifeHit = false;
        }
    }
}
