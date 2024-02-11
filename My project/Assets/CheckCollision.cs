using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;

    new Collider2D collider;

    CollisionPhysics collisionPhysics;

    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        collisionPhysics = GetComponent<CollisionPhysics>();
    }
    void CheckSlope()
    {
        bool isSlope = Player.GetComponent<PlayerControl>().isSlope;
        if (isSlope)
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void CheckCollisionType()
    {
        if (PistolBulletHit)
        {
            collisionPhysics.SetPhysics(new Vector2(0, 0));
        }
        if (RifleBulletHit)
        {
            collisionPhysics.SetPhysics(new Vector2(0, 0));
        }
        if (KnifeHit)
        {
            collisionPhysics.SetPhysics(new Vector2(0, 0));
        }
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

    // Update is called once per frame
    void Update()
    {
        CheckCollisionType();
        if (transform.parent.name == "bone_11" ||
            transform.parent.name == "bone_9")
            CheckSlope();
        //Debug.Log(KnifeHit);
    }
}
