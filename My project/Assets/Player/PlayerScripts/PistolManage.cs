using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PistolManage : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public GameObject WeaponUI;

    ParticleSystem PS;

    private bool _AmmunitionZero;
    private bool _isReload;
    private bool _isReloading;
    private bool _isShot;
    public bool isReload
    {
        get { return _isReload; }
        set { _isReload = value; }
    }
    public bool isReloading { 
        get { return _isReloading; }
        set { _isReloading = value; }
    }
    public bool isShot { 
        get { return _isShot; }
        set { _isShot = value; }
    }
    public bool AmmunitionZero
    {
        get { return _AmmunitionZero; }
        set { _AmmunitionZero = value; }
    }

    public float BulletDamage;
    public float ShotDelay = 0.7f;
    public int curAmmunition;
    public int maxAmmunition;

    public int curMagazines;

    public float Reload_Delay;

    PlayerControl playControl;
    Animator playerAnim;
    void Start()
    {
        PS = transform.GetChild(0).GetComponent<ParticleSystem>();
        curAmmunition = maxAmmunition;
        curMagazines = 1;
        playControl = Player.GetComponent<PlayerControl>();
        playerAnim = Player.GetComponent<Animator>();  
    }

    public void ResetMagazine()
    {
        curAmmunition = maxAmmunition;
        curMagazines = 1;
    }

    void Shot()
    {
        if (playControl.ShotDelayElapsed == 0 &&
            playControl.isShooting &&
            playControl.weapon == PlayerControl.Weapons.PISTOL)
        {
            PS.Play();
            PS.Emit(20);
            
            _isShot = true;
            if (curAmmunition != 0)
            {
                curAmmunition -= 1;
            }

            if (curAmmunition == 0)
            {
                _AmmunitionZero = true;
            }
        }
        else 
        {
            _isShot = false;
            PS.Stop();
        }
            

    }
    public void GetMagazine()
    {
        if (curMagazines < 3)
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
                _AmmunitionZero = false;
                _isReload = false;
                _isReloading = false;
            }
            else
            {
                _isReloading = true;
            }
        }
    }

    public void ReloadReset()
    {
        _isReloading = false;
        _isReload = false;
    }

    // Update is called once per frame
    void Update()         
    {
        Shot();
        if (curMagazines > 0 && _AmmunitionZero) 
        {
            _isReload = true;
            Reload();
        }
    }
}
