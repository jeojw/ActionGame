using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update

    CustomCollider2D CCollider;
    PlayerControl Control;
    StatManage PlayerStat;
    CollisionPhysics Physic;

    private bool _isHit = false;
    private bool KnifeHit = false;
    private bool PistolBulletHit = false;
    private bool RifleBulletHit = false;

    public bool isHit { 
        get { return _isHit; }
        set { _isHit = value; }
    }

    private float HitCoolElapsed = 0;
    private float HitCoolStart = 0;
    private float HitCool = 1f;

    void Start()
    {
        Control = GetComponent<PlayerControl>();
        PlayerStat = GetComponent<StatManage>();
        CCollider = GetComponent<CustomCollider2D>();
        Physic = GetComponent<CollisionPhysics>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CCollider.enabled)
        {
            if (collision.CompareTag("Enemy_Pistol_Bullet"))
            {
                PistolBulletHit = true;
            }

            if (collision.CompareTag("Enemy_Rifle_Bullet"))
            {
                RifleBulletHit = true;
            }

            if (collision.CompareTag("EnemyKnife"))
            {
                KnifeHit = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!PlayerStat.isDead)
        {
            _isHit = (KnifeHit || PistolBulletHit || RifleBulletHit);
            if (_isHit)
            {
                CCollider.enabled = false;
                HitCoolStart = Time.time;
                if (PistolBulletHit)
                {
                    Physic.SetPhysics(new Vector2((int)Control.direction * (-1) * 50, 0));
                    PlayerStat.SetGetDamage(200f);
                    PistolBulletHit = false;
                }

                if (RifleBulletHit)
                {
                    Physic.SetPhysics(new Vector2((int)Control.direction * (-1) * 80, 0));
                    PlayerStat.SetGetDamage(350f);
                    RifleBulletHit = false;
                }

                if (KnifeHit)
                {
                    Physic.SetPhysics(new Vector2((int)Control.direction * (-1) * 15, 0));
                    PlayerStat.SetGetDamage(150f);
                    KnifeHit = false;
                }
            }
            else
            {
                HitCoolElapsed = Time.time - HitCoolStart;
                if (HitCoolElapsed >= HitCool)
                {
                    CCollider.enabled = true;
                    HitCoolElapsed = 0;
                    HitCoolStart = 0;
                }
            }
        }
        else
        {
            CCollider.enabled = false;
            Physic.SetPhysics(new Vector2(0, 0));
        }
    }
}
