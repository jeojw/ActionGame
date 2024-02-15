using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpriteUpdate : MonoBehaviour
{
    public GameObject Player;
    public Animator anim;

    public GameObject Pistol;
    public GameObject Rifle;
    public MeshRenderer Knife;

    PlayerControl.Direction Direct = PlayerControl.Direction.RIGHT;
    bool JumpStart = false;
    bool Jumping = false;
    bool Running = false;
    bool Walking = false;
    bool Landing = false;
    bool OnGround = false;
    bool Rolling = false;
    bool LowerBody = false;

    PlayerControl playerControl;
    GrapplingHook graphook;
    SpriteRenderer pistolSprite;
    GunManage pistolManage;
    StatManage statManage;

    // Start is called before the first frame update
    void Start()
    {
        pistolSprite = Pistol.GetComponent<SpriteRenderer>();
        pistolSprite.enabled = true;
        Rifle.GetComponent<SpriteRenderer>().enabled = false;
        Knife.enabled = false;
        playerControl = GetComponent<PlayerControl>();
        graphook = GetComponent<GrapplingHook>();
        pistolManage = Pistol.GetComponent<GunManage>();
        statManage = GetComponent<StatManage>();
    }

    void MoveSprite()
    {
        
        Direct = playerControl.direction;
        Jumping = playerControl.isJump;
        JumpStart = playerControl.isJumpStart;
        Running = playerControl.isRunning;
        Walking = playerControl.isWalking;
        Landing = playerControl.isLanding;
        OnGround = playerControl.isGround;
        Rolling = playerControl.isRolling;
        LowerBody = playerControl.isLowerBody;

        if (Direct == PlayerControl.Direction.LEFT) { Player.transform.localScale = new Vector3(-1, 1, 1); }
        else if (Direct == PlayerControl.Direction.RIGHT) { Player.transform.localScale = new Vector3(1, 1, 1); }

        if (playerControl.weapon == PlayerControl.Weapons.GUNS)
        {
            anim.SetBool("isGetPistol", true);
        }
        else
        {
            anim.SetBool("isGetPistol", false);
        }

        if (Walking)
        {   
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Running) 
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (JumpStart) 
        {
            anim.SetBool("isJumpStart", true); 
        }
        else
        {
            anim.SetBool("isJumpStart", false);
        }
        if (Jumping)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        if (Landing)
        {
            anim.SetBool("isLanding", true);
        }
        else
        {
            anim.SetBool("isLanding", false);
        }
        if (OnGround)
        {
            anim.SetBool("isLand", true);
            anim.SetBool("isStanding", true);
        }
        else
        {
            anim.SetBool("isLand", false);
            anim.SetBool("isStanding", false);
        }
        if (Rolling)
        {
            anim.SetBool("isRolling", true);
            anim.Play("Rolling");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            anim.SetBool("isRolling", false);
        }

        if (LowerBody)
        {
            anim.SetBool("isLowerBody", true);
        }
        else
        {
            anim.SetBool("isLowerBody", false);
        }

        if (statManage.isDead)
            anim.SetBool("isDead", true);
    }

    void WeaponSprite()
    {
        if (playerControl.weapon == PlayerControl.Weapons.NONE)
        {
            if (playerControl.isPunching)
                anim.SetBool("isPunching", true);
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    anim.SetBool("isPunching", false);
            }
        }

        if (playerControl.weapon == PlayerControl.Weapons.GUNS)
        {
            pistolSprite.enabled = true;
            if (!pistolManage.AmmunitionZero)
            {
                anim.SetBool("isReloading", false);
                
            }
            else
                anim.SetBool("isReloading", true);
            if (playerControl.isShooting)
            {
                anim.SetBool("isPistolShooting", true);
                //if (GameObject.Find("UI").GetComponent<WeaponUIManage>().isZero)
                //{
                //    anim.SetBool("isReloading", true);
                //    GameObject.Find("UI").GetComponent<WeaponUIManage>().isZero = false;
                //}
                //else
                //{
                //    anim.SetBool("isReloading", false);
                //    GameObject.Find("UI").GetComponent<WeaponUIManage>().isReload = true;
                //}
            }
            else
            {
                anim.SetBool("isPistolShooting", false);
            }

        }
        else
        {
            pistolSprite.enabled = false;
        }

        if (playerControl.weapon == PlayerControl.Weapons.KNIFE)
        {
            anim.SetBool("isKnifeFightReady", true);
            Knife.enabled = true;
        }

        else
        {
            anim.SetBool("isKnifeFightReady", false);
            Knife.enabled = false;
        }  

        if (playerControl.isFencing)
        {
            if (playerControl.KnifeStep == 1)
            {
                anim.SetBool("isFencing_1", true);
            }
            if (playerControl.KnifeStep == 2)
            {
                anim.SetBool("isFencing_2", true);
            }
            if (playerControl.KnifeStep == 3)
            {
                anim.SetBool("isFencing_3", true);
            }
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_1"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    anim.SetBool("isFencing_1", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_2"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    anim.SetBool("isFencing_2", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_3"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    anim.SetBool("isFencing_3", false);
            }
        }
        
        if (playerControl.weapon == PlayerControl.Weapons.ROPE)
        {
            anim.SetBool("isRopeReady", true);
        }
        else
        {
            anim.SetBool("isRopeReady", false);
        }
        if (graphook.isHookActive)
        {
            anim.SetBool("isHookActive", true);
            if (graphook.isAttach)
            {
                anim.SetBool("isAttach", true);
                if (playerControl.isShooting)
                    anim.SetBool("isHookShooting", true);
                else
                    anim.SetBool("isHookShooting", false);
            }
        }
        else
        {
            anim.SetBool("isHookActive", false);
            anim.SetBool("isAttach", false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        MoveSprite();
        WeaponSprite();
    }
}