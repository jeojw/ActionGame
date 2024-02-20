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

    private bool Walking;
    private bool Fencing;
    private bool Shooting;
    private bool isDead;
    private bool GetHit;
    private bool isDetect;
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
            anim.SetBool("isGetPistol", true);
            if (Shooting)
                anim.SetBool("isShot", true);
            else
                anim.SetBool("isShot", false);
        }
        else
            PistolSprite.enabled = false;

        if (Control.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            RifleSprite.enabled = true;
            anim.SetBool("isGetRifle", true);
            if (Shooting)
                anim.SetBool("isShot", true);
            else
                anim.SetBool("isShot", false);
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
        Shooting = Control.isShooting;
        isDead = Stat.isDead;
        GetHit = Stat.GetHit;
        isDetect = Control.isDetect;
        if (!isDead)
        {
            if (isDetect)
                anim.SetBool("isDetect", true);
            else
                anim.SetBool("isDetect", false);
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
            anim.Play("Dead");
        }
    }
}
