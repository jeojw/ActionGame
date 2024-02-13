using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public PolygonCollider2D collider;
    CollisionPhysics Physic;
    Enemy_Movement EMove;
    Enemy_StatManage Estat;

    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;
    public bool PisteHit = false;

    public bool isHit = false;
    private bool isDead;

    private float HitDelay = 1f;
    private float HitDelayStart = 0;
    private float HitDelayElapsed = 0;

    void Start()
    {
        Estat = Enemy.GetComponent<Enemy_StatManage>();
        Physic = Enemy.GetComponent<CollisionPhysics>();
        collider = GetComponent<PolygonCollider2D>();
        isDead = Enemy.GetComponent<Enemy_StatManage>().isDead;
        EMove = Enemy.GetComponent<Enemy_Movement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitDelayElapsed == 0)
        {
            if (collision.CompareTag("Piste"))
            {
                PisteHit = true;
            }
            if (collision.CompareTag("Pistol_Bullet"))
            {
                PistolBulletHit = true;
            }

            if (collision.CompareTag("Rifle_Bullet"))
            {
                RifleBulletHit = true;
            }

            if (collision.CompareTag("Knife"))
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
            collider.enabled = true;
            HitDelayElapsed = 0;
            HitDelayStart = 0;
        }
    }

    private void Update()
    {
        isDead = Estat.isDead;
        if (!isDead)
        {
            isHit = (KnifeHit || RifleBulletHit || PistolBulletHit || PisteHit);
            if (isHit)
            {
                collider.enabled = false;
                HitDelayStart = Time.time;
                if (PistolBulletHit)
                {
                    Physic.SetPhysics(new Vector2((int)EMove.detectDirection * (-1) * 80, 0));
                    PistolBulletHit = false;
                }

                if (PisteHit)
                {
                    Physic.SetPhysics(new Vector2((int)EMove.detectDirection * (-1) * 20, 0));
                    PisteHit = false;
                }

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
        else
            collider.enabled = false;
            
        Debug.Log(HitDelayElapsed);
    }
}
