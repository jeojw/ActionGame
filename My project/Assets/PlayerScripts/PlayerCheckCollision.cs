using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pistol_Bullet"))
        {

        }

        else if (collision.CompareTag("Rifle_Bullet"))
        {

        }

        else if (collision.CompareTag("Knife"))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckSlope();
    }
}
