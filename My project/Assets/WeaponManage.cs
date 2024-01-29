using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManage : MonoBehaviour
{
    // Start is called before the first frame update\

    public GameObject Player;

    public bool Cooldown;
    public bool isShooting;
    public bool MagazineZero;

    public float Pistol_Shot_Delay;
    private float Pistol_Delay_Cur = 0;
    private float Pistol_Delay_Start = 0;
    public int PistolcurMagazine;
    public int PistolmaxMagazine;

    public int RiflelcurMagazine;
    public int RiflelmaxMagazine;
    public float Rifle_Shot_Delay;

    public float Reload_Delay;
    void Start()
    {        
        Pistol_Delay_Cur = 0;
        PistolcurMagazine = PistolmaxMagazine;
    }

    void Pistol_Shot()
    {
        
        if (Player.GetComponent<Movement>().delayElapsed == 0)
        {

            if (PistolcurMagazine > 0)
                PistolcurMagazine--;
            else
            {
                MagazineZero = true;
            }                
        }
        
    }

    void Pistol_Reload()
    {
        if (MagazineZero)
        {
            Invoke("Pistol_Shot", 3f);
            PistolcurMagazine = PistolmaxMagazine;
            MagazineZero = false;
        }
    }

    void Reload()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pistol_Shot();
        //Pistol_Reload();
    }
}
