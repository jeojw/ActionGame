using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Update : MonoBehaviour
{
    Animator anim;
    Enemy_Movement Control;
    Enemy_StatManage Stat;
    MeshRenderer KnifeSprite;

    public GameObject Knife;

    bool Walking;
    bool Fencing;
    bool isDead;
    bool GetHit;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
        Stat = GetComponent<Enemy_StatManage>();
        Control = GetComponent<Enemy_Movement>();
        KnifeSprite = Knife.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Walking = Control.isWalking;
        Fencing = Control.isFencing;
        isDead = Stat.isDead;
        GetHit = Stat.GetHit;
        if (!isDead)
        {
            if (Walking)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            if (Control.AttackType == Enemy_Movement.ATTACKTYPE.SWORD)
            {
                KnifeSprite.enabled = true;
                if (Fencing)
                {
                    anim.SetBool("isFencing", true);
                }
                else
                {
                    anim.SetBool("isFencing", false);
                }
            }
            else
                KnifeSprite.enabled = false;
            
            if (GetHit)
            {
                anim.SetBool("GetHit", true);
            }
            else
            {
                anim.SetBool("GetHit", false);
            }
        }
        else
        {
            anim.SetBool("isDead", true);
        }
    }
}
