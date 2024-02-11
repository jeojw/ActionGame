using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManage : MonoBehaviour
{
    // Start is called before the first frame update\

    public GameObject Player;
    public GameObject WeaponUI;
    public AudioSource WeaponSound;
    public AudioSource ReloadSound;

    public bool AmmunitionZero;

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
            if (curAmmunition != 0)
            {
                curAmmunition -= 1;
                WeaponSound.Play();
            }

            if (curAmmunition == 0)
            {
                AmmunitionZero = true;
            }
        }

    }

    void Reload()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol"))
        {
            if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                curAmmunition = maxAmmunition;
                AmmunitionZero = false;
                ReloadSound.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()         
    {
        Shot();
        if (AmmunitionZero)
            Reload();
    }
}
