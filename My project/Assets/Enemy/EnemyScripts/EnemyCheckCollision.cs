using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public PolygonCollider2D Pcollider;
    CollisionPhysics Physic;
    Enemy_Movement EMove;
    Enemy_StatManage Estat;

    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;
    public bool FistHit = false;

    public bool isHit = false;
    private bool isDead;

    private float HitDelay = 0.4f;
    private float HitDelayStart = 0;
    private float HitDelayElapsed = 0;

    void Start()
    {
        Estat = Enemy.GetComponent<Enemy_StatManage>();
        Physic = Enemy.GetComponent<CollisionPhysics>();
        Pcollider = GetComponent<PolygonCollider2D>();
        isDead = Enemy.GetComponent<Enemy_StatManage>().isDead;
        EMove = Enemy.GetComponent<Enemy_Movement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitDelayElapsed == 0)
        {
            if (collision.CompareTag("Fist"))
            {
                FistHit = true;
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
    private void RaycastHit()
    {
        RaycastHit2D Hit;
        if (transform.parent.name != "bone_1")
        {
            Hit = Physics2D.BoxCast(transform.position, new Vector2(0.75f, 1.7f), transform.parent.transform.localRotation.z, transform.forward, 0.1f);
        }
        else
            Hit = Physics2D.CircleCast(transform.position, 1.11f, transform.forward, 0.1f);
        if (Hit.collider != null)
        {
            if (Hit.collider.tag == "Fist")
                FistHit = true;
            if (Hit.collider.tag == "Pistol_Bullet")
                PistolBulletHit = true;
            if (Hit.collider.tag == "Rifle_Bullet")
                RifleBulletHit = true;
            if (Hit.collider.tag == "Knife")
                KnifeHit = true;
        }
    }

    void HitDelayProcess()
    {
        HitDelayElapsed = Time.time - HitDelayStart;

        if (HitDelayElapsed >= HitDelay)
        {
            Pcollider.enabled = true;
            HitDelayElapsed = 0;
            HitDelayStart = 0;
        }
    }

    void Update()
    {
        
        isDead = Estat.isDead;
        if (!isDead)
        {
            RaycastHit();
            isHit = (KnifeHit || RifleBulletHit || PistolBulletHit || FistHit);
            if (isHit)
            {
                Pcollider.enabled = false;
                HitDelayStart = Time.time;
                if (PistolBulletHit)
                {
                    Physic.SetPhysics(new Vector2((int)EMove.detectDirection * (-1) * 70, 0));
                    Estat.SetDamage(75f);
                    PistolBulletHit = false;
                }

                if (FistHit)
                {
                    Physic.SetPhysics(new Vector2((int)EMove.detectDirection * (-1) * 15, 0));
                    Estat.SetDamage(30f);
                    FistHit = false;
                }

                if (KnifeHit)
                {
                    Physic.SetPhysics(new Vector2((int)EMove.detectDirection * (-1) * 15, 0));
                    Estat.SetDamage(75f);
                    KnifeHit = false;
                }

                if (RifleBulletHit)
                {
                    Physic.SetPhysics(new Vector2((int)EMove.detectDirection * (-1) * 90, 0));
                    Estat.SetDamage(150f);
                    RifleBulletHit = false;
                }
            }
            else
            {
                HitDelayProcess();
            }
        }
        else
        {
            Physic.SetPhysics(new Vector2(0, 0));
            Pcollider.enabled = false;
        }
    }
}
