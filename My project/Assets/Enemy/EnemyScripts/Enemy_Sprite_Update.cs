using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Update : MonoBehaviour
{
    Animator anim;

    bool Walking;
    bool Fencing;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        Walking = GetComponent<Enemy_Movement>().isWalking;
        Fencing = GetComponent<Enemy_Movement>().isFencing;
        if (Walking)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Fencing)
        {
            anim.SetBool("isFencing", true);
        }
        else
        {
            anim.SetBool("isFencing", false);
        }
    }
}
