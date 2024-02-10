using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public CircleCollider2D HeadHitbox;
    public EdgeCollider2D BodyHitbox_1;
    public EdgeCollider2D BodyHitbox_2;
    public EdgeCollider2D LeftLegHitbox_1;
    public EdgeCollider2D LeftLegHitbox_2;
    public EdgeCollider2D RightLegHitbox_1;
    public EdgeCollider2D RightLegHitbox_2;

    public bool KnifeHit = false;
    public bool PistolBulletHit = false;
    public bool RifleBulletHit = false;

    void Start()
    {

    }
    void CheckSlope()
    {
        bool isSlope = GetComponent<PlayerControl>().isSlope;
        if (isSlope)
        {
            LeftLegHitbox_2.isTrigger = true;
            RightLegHitbox_2.isTrigger = true;
        }
        else
        {
            LeftLegHitbox_2.isTrigger = false;
            RightLegHitbox_2.isTrigger = false;
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
        CheckSlope();
        //Debug.Log(KnifeHit);
    }
}
