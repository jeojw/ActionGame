using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManage : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public GameObject WeaponUI;

    ParticleSystem PS;

    public bool AmmunitionZero;
    public bool isShot;
    public bool isReload;
    public bool isReloading;

    public float BulletDamage;
    public float ShotDelay = 0.01f;
    public int curAmmunition;
    public int maxAmmunition;
    public int curMagazines;

    PlayerControl playControl;
    Animator playerAnim;
    void Start()
    {
        PS = transform.GetChild(0).GetComponent<ParticleSystem>();
        playControl = Player.GetComponent<PlayerControl>();
        playerAnim = playControl.GetComponent<Animator>();
        curAmmunition = maxAmmunition;
        curMagazines = 1;
        AmmunitionZero = false;
    }

    public void ResetMagazine()
    {
        curAmmunition = maxAmmunition;
        curMagazines = 1;
    }

    public void GetMagazine()
    {
        if (curMagazines < 3)
        {
            curMagazines++;
        }
    }

    void Shot()
    {
        if (playControl.ShotDelayElapsed == 0 &&
            playControl.isShooting &&
            playControl.weapon == PlayerControl.Weapons.RIFLE)
        {
            PS.Play();
            PS.Emit(20);
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
        {
            isShot = false;
            PS.Stop();
        }

    }

    void Reload()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Rifle"))
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
        if (curMagazines > 0 && AmmunitionZero)
        {
            isReload = true;
            Reload();
        }
        
    }
}

