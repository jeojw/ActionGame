using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUIManage : MonoBehaviour
{
    private TextMeshProUGUI Pistol_Ammunition;
    private TextMeshProUGUI Rifle_Ammunition;
    private TextMeshProUGUI Pistol_Magazines;
    private TextMeshProUGUI Rifle_Magazines;
    public GameObject PlayerInfo;
    public GameObject Pistol;
    public GameObject Rifle;

    SceneUI AllUI;
    PistolManage PistolManage;
    RifleManage RifleManage;

    private int Pistol_Cur_Ammunition;
    private int Pistol_Cur_Magazines;
    private int Rifle_Cur_Ammunition;
    private int Rifle_Cur_Magazines;

    private bool _isZeroPistol = false;
    private bool _isZeroRifle = false;

    public bool IsZeroPistol {
        get { return _isZeroPistol; }
        set { _isZeroPistol = value; }
    }
    public bool IsZeroRifle { 
        get { return _isZeroRifle; }
        set { _isZeroRifle = value; }
    }

    void Start()
    {
        AllUI = GetComponent<SceneUI>();
        PistolManage = Pistol.GetComponent<PistolManage>();
        RifleManage = Rifle.GetComponent<RifleManage>();
    }

    void Pistol_Manage()
    {
        Pistol_Ammunition = AllUI.Pistol_Ammunition_Text;
        Pistol_Magazines = AllUI.Pistol_Magazine_Text;
        Pistol_Cur_Ammunition = PistolManage.curAmmunition;
        Pistol_Cur_Magazines = PistolManage.curMagazines;

        _isZeroPistol = (Pistol_Cur_Ammunition == 0);
        
        Pistol_Ammunition.text = "X " + Pistol_Cur_Ammunition.ToString();
        Pistol_Magazines.text = "X " + Pistol_Cur_Magazines.ToString();
    }


    void Rifle_Manage()
    {
        Rifle_Ammunition = AllUI.Rifle_Ammunition_Text;
        Rifle_Magazines = AllUI.Rifle_Magazine_Text;
        Rifle_Cur_Ammunition = RifleManage.curAmmunition;
        Rifle_Cur_Magazines = RifleManage.curMagazines;

        _isZeroRifle = (Rifle_Cur_Ammunition == 0);

        Rifle_Ammunition.text = "X " + Rifle_Cur_Ammunition.ToString();
        Rifle_Magazines.text = "X " + Rifle_Cur_Magazines.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Pistol_Manage();
        Rifle_Manage();
    }
}
