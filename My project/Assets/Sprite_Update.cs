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
    Animation curSprite;

    public SpriteRenderer Pistol;
    public SpriteRenderer Rifle;
    public MeshRenderer Knife;

    bool isSwitching = false;
    Movement.Direction Direct = Movement.Direction.RIGHT;
    bool JumpStart = false;
    bool Jumping = false;
    bool Running = false;
    bool Walking = false;
    bool Landing = false;
    bool OnGround = false;

    IEnumerator WaitforAnimationToFinish()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife Fighting_1"))
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    }
    // Start is called before the first frame update
    void Start()
    {
        Pistol.enabled = true;
        Rifle.enabled = false;
        Knife.enabled = false;
    }

    void MoveSprite()
    {
        
        Direct = Player.GetComponent<Movement>().direction;
        Jumping = Player.GetComponent<Movement>().isJump;
        JumpStart = Player.GetComponent<Movement>().isJumpStart;
        Running = Player.GetComponent<Movement>().isRunning;
        Walking = Player.GetComponent<Movement>().isWalking;
        Landing = Player.GetComponent<Movement>().isLanding;
        OnGround = Player.GetComponent<Movement>().isGround;

        if (Direct == Movement.Direction.LEFT) { Player.transform.localScale = new Vector3(-1, 1, 1); }
        else if (Direct == Movement.Direction.RIGHT) { Player.transform.localScale = new Vector3(1, 1, 1); }

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
            anim.Play("Jump_start");
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
    }

    void WeaponSprite()
    {
        if (GetComponent<Movement>().isShooting)
        {
            anim.SetBool("isPistolShooting", true);
            Pistol.enabled = true;
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
            Pistol.enabled = false;
        }

        if (GetComponent<Movement>().isFencing)
        {
            anim.Play("Knife_Fighting_1");
            anim.SetBool("isFencing_1", true);
            Knife.enabled = true;
        }
        else
        {
            anim.SetBool("isFencing_1", false);
            WaitforAnimationToFinish();
            Knife.enabled = false;
        }

        if (transform.GetComponent<GrapplingHook>().isHookActive)
        {
            anim.SetBool("isHookActive", true);
            if (transform.GetComponent<GrapplingHook>().isAttach)
            {
                anim.SetBool("isAttach", true);
                if (GetComponent<Movement>().isShooting)
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
