using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    CustomCollider2D Ccollider;
    CollisionPhysics Physic;
    Enemy_Movement EMove;
    Enemy_StatManage Estat;

    private bool KnifeHit = false;
    private bool PistolBulletHit = false;
    private bool RifleBulletHit = false;
    private bool FistHit = false;

    private bool _isHit = false;

    public bool isHit
    {
        get { return _isHit; }
        set { _isHit = value; }
    }
    private bool isDead;

    private float HitDelay = 0.2f;
    private float HitDelayStart = 0; 
    private float HitDelayElapsed = 0;

    void Start()
    {
        Estat = GetComponent<Enemy_StatManage>();
        Physic = GetComponent<CollisionPhysics>();
        Ccollider = GetComponent<CustomCollider2D>();
        isDead = GetComponent<Enemy_StatManage>().isDead;
        EMove = GetComponent<Enemy_Movement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Ccollider.enabled)
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

            if (collision.CompareTag("PlayerKnife"))
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
            Ccollider.enabled = true;
            HitDelayElapsed = 0;
            HitDelayStart = 0;
        }
    }

    void Update()
    {
        
        isDead = Estat.isDead;
        if (!isDead)
        {
            _isHit = (KnifeHit || RifleBulletHit || PistolBulletHit || FistHit);
            if (_isHit)
            {
                Ccollider.enabled = false;
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
            Ccollider.enabled = false;
        }
    }
}
