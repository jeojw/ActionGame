using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistolManage : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public GameObject WeaponUI;

    public bool AmmunitionZero;
    public bool isReload;
    public bool isReloading;
    public bool isShot;

    public float BulletDamage;
    public float ShotDelay = 0.7f;
    public float curAmmunition;
    public float maxAmmunition;

    public float Reload_Delay;

    PlayerControl playControl;
    Animator playerAnim;
    void Start()
    {
        curAmmunition = maxAmmunition;
        playControl = Player.GetComponent<PlayerControl>();
        playerAnim = Player.GetComponent<Animator>();  
    }

    public void ResetMagazine()
    {
        if (AmmunitionZero)
        {
            isReload = true;
            AmmunitionZero = false;
        }
         
        else
        {
            curAmmunition = maxAmmunition;
        }
        
       
    }

    void Shot()
    {
        if (playControl.ShotDelayElapsed == 0 &&
            playControl.isShooting &&
            playControl.weapon == PlayerControl.Weapons.PISTOL)
        {
            isShot = true;
            if (curAmmunition != 0)
            {
                curAmmunition -= 1;
            }

            if (curAmmunition == 0)
            {
                AmmunitionZero = true;
            }
        }
        else
            isShot = false;

    }

    void Reload()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol"))
        {
            if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                curAmmunition = maxAmmunition;
                AmmunitionZero = false;
                isReload = false;
                isReloading = false;
            }
            else
            {
                isReloading = true;
            }
        }
    }

    public void ReloadReset()
    {
        isReloading = false;
        isReload = false;
    }

    // Update is called once per frame
    void Update()         
    {
        Shot();
        if (isReload) {
            Reload();
        }
            
    }
}
