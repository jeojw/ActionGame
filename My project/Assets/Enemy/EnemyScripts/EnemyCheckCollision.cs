using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    PolygonCollider2D Pcollider;
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
        if (Pcollider.enabled)
        {
            FistHit = collision.CompareTag("Fist");

            PistolBulletHit = collision.CompareTag("Pistol_Bullet");

            RifleBulletHit = collision.CompareTag("Rifle_Bullet");

            KnifeHit = collision.CompareTag("Knife");
        }
    }
    private void RaycastHit()
    {
        if (Pcollider.enabled)
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
                FistHit = Hit.collider.tag == "Fist";
                PistolBulletHit = Hit.collider.tag == "Pistol_Bullet";
                RifleBulletHit = Hit.collider.tag == "Rifle_Bullet";
                KnifeHit = Hit.collider.tag == "Knife";
            }
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
            _isHit = (KnifeHit || RifleBulletHit || PistolBulletHit || FistHit);
            if (_isHit)
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
