using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Update : MonoBehaviour
{
    Animator anim;
    Enemy_Movement Control;

    bool Walking;
    bool Fencing;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
        Control = GetComponent<Enemy_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Walking = Control.isWalking;
        Fencing = Control.isFencing;
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
