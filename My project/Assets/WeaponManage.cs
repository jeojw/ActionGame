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
    void Start()
    {
        curAmmunition = maxAmmunition;
    }

    void Shot()
    {

        if (Player.GetComponent<Movement>().delayElapsed == 0 &&
            Player.GetComponent<Movement>().isShooting)
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
        if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol"))
        {
            if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
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
