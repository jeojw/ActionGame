using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUIManage : MonoBehaviour
{
    private TextMeshProUGUI Pistol_Magazine;
    private bool isShoot;
    public bool isReload;
    public bool isZero;

    public int Pistol_Cur_Magazine;
    public int Pistol_Max_Magazine;

    public int Rifle_Cur_Magazine;
    public int Rifle_Max_Magazine;

    public bool IsZeroPistol = false;
    public bool IsZeroRifle = false;

    void Start()
    {
        Pistol_Cur_Magazine = Pistol_Max_Magazine;
        Rifle_Cur_Magazine = Rifle_Max_Magazine;
    }

    void Pistol()
    {
        Pistol_Magazine = GetComponent<UI>().Pistol_Magazine_Text;
        isShoot = GameObject.Find("Player").GetComponent<Movement>().isShooting;
        if (isShoot)
        {
            if (Pistol_Cur_Magazine != 0)
            {
                Pistol_Cur_Magazine--;
            }
            else
            {
                isZero = true;
                if (isReload)
                {
                    Pistol_Cur_Magazine = Pistol_Max_Magazine;
                }
            }
        }
        Pistol_Magazine.text = "X " + Pistol_Cur_Magazine.ToString();
    }


    void Rifle()
    {
        isShoot = GameObject.Find("Player").GetComponent<Movement>().isShooting;
        if (isShoot)
        {
            if (Pistol_Cur_Magazine != 0)
            {
                Pistol_Cur_Magazine--;
            }
            else
            {
                isZero = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Pistol();
        Rifle();
    }
}
