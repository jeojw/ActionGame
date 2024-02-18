
using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;

    PolygonCollider2D collider;
    CollisionPhysics collisionPhysics;
    PlayerControl Control;
    StatManage PlayerStat;
    PlayerControl.Weapons curWeapon;
    CollisionPhysics Physic;

    public bool isHit = false;
    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;
    string objectName;


    private float HitCoolElapsed = 0;
    private float HitCoolStart = 0;
    private float HitCool = 1f;

    bool isSlope;

    void Start()
    {
        collisionPhysics = Player.GetComponent<CollisionPhysics>();
        Control = Player.GetComponent<PlayerControl>();
        PlayerStat = Player.GetComponent<StatManage>();
        objectName = transform.name;
        collider = GetComponent<PolygonCollider2D>();
        curWeapon = Player.GetComponent<PlayerControl>().weapon;
        Physic = Player.GetComponent<CollisionPhysics>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitCoolElapsed == 0)
        {
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
    // Update is called once per frame
    void Update()
    {
        isHit = (KnifeHit || PistolBulletHit || RifleBulletHit);
        if (isHit)
        {
            collider.enabled = false;
            HitCoolStart = Time.time;
            if (PistolBulletHit)
            {
                Physic.SetPhysics(new Vector2((int)Control.direction * (-1) * 80, 0));
                PistolBulletHit = false;
            }

            if (KnifeHit)
            {
                Physic.SetPhysics(new Vector2((int)Control.direction * (-1) * 10, 0));
                KnifeHit = false;
            }
        }
        else
        {
            HitCoolElapsed = Time.time - HitCoolStart;
            if (HitCoolElapsed >= HitCool)
            {
                collider.enabled = true;
                HitCoolElapsed = 0;
                HitCoolStart = 0;
            }
        }
    }
}
