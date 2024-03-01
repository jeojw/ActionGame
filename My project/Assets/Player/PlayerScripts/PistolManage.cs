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
    private bool _isReload;
    private bool _isReloading;
    public bool isReload
    {
        get { return _isReload; }
        set { _isReload = value; }
    }
    public bool isReloading { 
        get { return _isReloading; }
        set { _isReloading = value; }
    }
    public bool isShot;

    public float BulletDamage;
    public float ShotDelay = 0.7f;
    public float curAmmunition;
    public float maxAmmunition;

    public float curMagazines;

    public float Reload_Delay;

    PlayerControl playControl;
    Animator playerAnim;
    void Start()
    {
        curAmmunition = maxAmmunition;
        curMagazines = 0;
        playControl = Player.GetComponent<PlayerControl>();
        playerAnim = Player.GetComponent<Animator>();  
    }

    public void ResetMagazine()
    {
        curAmmunition = maxAmmunition;
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
    public void GetMagazine()
    {
        if (curMagazines <= 3)
        {
            curMagazines++;
        }
            
    }

    void Reload()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol"))
        {
            if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (curMagazines > 0)
                    curMagazines--;
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
        if (curMagazines > 0 && AmmunitionZero) {
            isReload = true;
            Reload();
        }
    }
}
