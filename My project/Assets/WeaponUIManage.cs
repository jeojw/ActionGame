using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUIManage : MonoBehaviour
{
    private TextMeshProUGUI Pistol_Magazine;
    public GameObject PlayerInfo;
    public GameObject PistolManage;
    public GameObject RifleManage;

    private float Pistol_Cur_Magazine;

    public bool IsZeroPistol = false;
    public bool IsZeroRifle = false;

    void Start()
    {
        
    }

    void Pistol()
    {
        Pistol_Magazine = GetComponent<UI>().Pistol_Magazine_Text;
        Pistol_Cur_Magazine = PistolManage.GetComponent<WeaponManage>().curMagazine;
        if (Pistol_Cur_Magazine == 0)
            IsZeroPistol = true;
        else
            IsZeroPistol = false;

        Pistol_Magazine.text = "X " + Pistol_Cur_Magazine.ToString();
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
