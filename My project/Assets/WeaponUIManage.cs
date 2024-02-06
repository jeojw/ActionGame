using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUIManage : MonoBehaviour
{
    private TextMeshProUGUI Pistol_Ammunition;
    public GameObject PlayerInfo;
    public GameObject PistolManage;
    public GameObject RifleManage;

    private float Pistol_Cur_Ammunition;

    public bool IsZeroPistol = false;
    public bool IsZeroRifle = false;

    void Start()
    {
        
    }

    void Pistol()
    {
        Pistol_Ammunition = GetComponent<UI>().Pistol_Ammunition_Text;
        Pistol_Cur_Ammunition = PistolManage.GetComponent<WeaponManage>().curAmmunition;
        if (Pistol_Cur_Ammunition == 0)
            IsZeroPistol = true;
        else
            IsZeroPistol = false;

        Pistol_Ammunition.text = "X " + Pistol_Cur_Ammunition.ToString();
    }


    void Rifle()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Pistol();
        Rifle();
    }
}
