using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public PolygonCollider2D Collider;

    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;

    public bool isHit = false;
    private bool isDead;

    private float HitDelay = 1f;
    private float HitDelayStart = 0;
    private float HitDelayElapsed = 0;

    void Start()
    {
        Collider = GetComponent<PolygonCollider2D>();
        isDead = Enemy.GetComponent<Enemy_StatManage>().isDead;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HitDelayElapsed == 0)
        {
            if (collision.collider.CompareTag("Pistol_Bullet"))
            {
                PistolBulletHit = true;
            }

            if (collision.collider.CompareTag("Rifle_Bullet"))
            {
                RifleBulletHit = true;
            }

            if (collision.collider.CompareTag("Knife"))
            {
                KnifeHit = true;
            }
        }
        
    }

    void HitDelayProcess()
    {
        HitDelayElapsed = Time.time - HitDelayStart;

        if (HitDelayElapsed >= HitDelay)
        {
            Collider.enabled = true;
            HitDelayElapsed = 0;
            HitDelayStart = 0;
        }
    }

    private void Update()
    {
        isHit = (KnifeHit || RifleBulletHit || PistolBulletHit);
        if (isHit)
        {
            Collider.enabled = false;
            HitDelayStart = Time.time;
            if (PistolBulletHit)
                PistolBulletHit = false;
            if (KnifeHit)
                KnifeHit = false;
            if (RifleBulletHit)
                RifleBulletHit = false;
        }
        else
        {
            HitDelayProcess();
        }
    }
}
