using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUIManage : MonoBehaviour
{
    private TextMeshProUGUI Pistol_Ammunition;
    public GameObject PlayerInfo;
    public GameObject Pistol;
    public GameObject Rifle;

    SceneUI AllUI;
    GunManage PistolManage;

    private float Pistol_Cur_Ammunition;

    public bool IsZeroPistol = false;
    public bool IsZeroRifle = false;

    void Start()
    {
        AllUI = GetComponent<SceneUI>();
        PistolManage = Pistol.GetComponent<GunManage>();
    }

    void Pistol_Manage()
    {
        Pistol_Ammunition = AllUI.Pistol_Ammunition_Text;
        Pistol_Cur_Ammunition = PistolManage.curAmmunition;
        if (Pistol_Cur_Ammunition == 0)
            IsZeroPistol = true;
        else
            IsZeroPistol = false;

        Pistol_Ammunition.text = "X " + Pistol_Cur_Ammunition.ToString();
    }


    void Rifle_Manage()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Pistol_Manage();
        Rifle_Manage();
    }
}
