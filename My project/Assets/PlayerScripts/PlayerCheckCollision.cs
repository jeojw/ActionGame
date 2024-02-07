using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    CircleCollider2D HeadHitbox;
    CapsuleCollider2D BodyHitbox;
    CapsuleCollider2D LowerHitbox;
    
    void Start()
    {
        HeadHitbox = GetComponentsInChildren<CircleCollider2D>()[0];
        BodyHitbox = GetComponentsInChildren<CapsuleCollider2D>()[0];
        LowerHitbox = GetComponentsInChildren<CapsuleCollider2D>()[1];
    }
    void CheckSlope()
    {
        bool isSlope = Player.GetComponent<PlayerControl>().isSlope;
        if (isSlope)
        {
            LowerHitbox.isTrigger = true;
        }
        else
        {
            LowerHitbox.isTrigger = false;
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
