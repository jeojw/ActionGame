using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManage : MonoBehaviour
{
    // Start is called before the first frame update\
    public bool Cooldown;
    public bool isShooting;
    public bool MagazineZero;
    public float Pistol_Shot_Delay;
    private float Pistol_Delay_Cur = 0;

    public float Rifle_Shot_Delay;

    public float Reload_Delay;
    void Start()
    {
        Pistol_Delay_Cur = 0;
    }

    void Pistol_Shot()
    {
        isShooting = GetComponent<Movement>().isShooting;
        if (isShooting)
        {
            if (Pistol_Delay_Cur == 0)
            {
                Pistol_Delay_Cur = Time.time;
                Cooldown = false;
            }
            else if (Pistol_Delay_Cur > Pistol_Shot_Delay)
            {
                Pistol_Delay_Cur = 0;
                Cooldown = true;
            }
        }
    }

    void Pistol_Reload()
    {
    }

    void Rifle_Shot()
    {

    }

    void Reload()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pistol_Shot();
    }
}
