using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy_Sprite_Update : MonoBehaviour
{
    Animator anim;
    Enemy_Movement Control;
    Enemy_StatManage Stat;
    MeshRenderer KnifeSprite;
    SpriteRenderer PistolSprite;
    SpriteRenderer RifleSprite;

    public GameObject Knife;
    public GameObject Pistol;
    public GameObject Rifle;

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
        PistolSprite = Pistol.GetComponent<SpriteRenderer>();
        RifleSprite = Rifle.GetComponent<SpriteRenderer>();
    }

    void WeaponSpriteUpdate()
    {
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

        if (Control.AttackType == Enemy_Movement.ATTACKTYPE.PISTOL)
        {
            PistolSprite.enabled = true;
        }
        else
            PistolSprite.enabled = false;

        if (Control.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            RifleSprite.enabled = true;
        }
        else
            RifleSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSpriteUpdate();
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
