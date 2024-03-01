using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;

    PolygonCollider2D Pcollider;
    PlayerControl Control;
    StatManage PlayerStat;
    CollisionPhysics Physic;

    public bool isHit = false;
    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;

    private float HitCoolElapsed = 0;
    private float HitCoolStart = 0;
    private float HitCool = 1f;

    void Start()
    {
        Control = Player.GetComponent<PlayerControl>();
        PlayerStat = Player.GetComponent<StatManage>();
        Pcollider = GetComponent<PolygonCollider2D>();
        Physic = Player.GetComponent<CollisionPhysics>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitCoolElapsed == 0)
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

    private void RaycastHit()
    {
        RaycastHit2D Hit;
        if (transform.parent.name != "bone_1")
            Hit = Physics2D.BoxCast(transform.position, new Vector2(0.75f, 1.7f), transform.parent.transform.localRotation.z, transform.forward, 0.1f, LayerMask.GetMask("Player"));
        else
            Hit = Physics2D.CircleCast(transform.position, 1.11f, transform.forward, 0.1f, LayerMask.GetMask("Player"));
        if (Hit.collider != null)
        {
            if (Hit.collider.tag == "Enemy_Pistol_Bullet")
                PistolBulletHit = true;
            if (Hit.collider.tag == "Enemy_Rifle_Bullet")
                RifleBulletHit = true;
            if (Hit.collider.tag == "EnemuKnife")
                KnifeHit = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit();
        if (!PlayerStat.isDead)
        {
            isHit = (KnifeHit || PistolBulletHit || RifleBulletHit);
            if (isHit)
            {
                Pcollider.enabled = false;
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
                    Pcollider.enabled = true;
                    HitCoolElapsed = 0;
                    HitCoolStart = 0;
                }
            }
        }
        else
        {
            Pcollider.enabled = false;
            Physic.SetPhysics(new Vector2(0, 0));
        }
    }
}
