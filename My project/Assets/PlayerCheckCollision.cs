using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D Hitbox;
    
    void Start()
    {
        Hitbox = GetComponent<BoxCollider2D>();
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
        
    }
}
