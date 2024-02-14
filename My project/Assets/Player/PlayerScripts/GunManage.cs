using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManage : MonoBehaviour
{
    // Start is called before the first frame update\

    public GameObject Player;
    public GameObject WeaponUI;

    public bool AmmunitionZero;
    public bool isReload;
    public bool isShot;

    public int curAmmunition;
    public int maxAmmunition;

    public float Reload_Delay;

    PlayerControl playControl;
    Animator playerAnim;
    void Start()
    {
        curAmmunition = maxAmmunition;
        playControl = Player.GetComponent<PlayerControl>();
        playerAnim = Player.GetComponent<Animator>();  
    }

    void Shot()
    {

        if (playControl.ShotDelayElapsed == 0 &&
            playControl.isShooting)
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
        if (transform.tag == "Pistol")
        {
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol"))
            {
                if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    curAmmunition = maxAmmunition;
                    AmmunitionZero = false;
                    isReload = false;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()         
    {
        Shot();
        if (AmmunitionZero) {
            isReload = true;
            Reload();
        }
            
    }
}
