using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UIManage : MonoBehaviour
{
    public Slider Hpbar;
    public GameObject Enemy;
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject GunUI;

    TextMeshProUGUI BulletText;

    Enemy_GunManage PM;
    Enemy_GunManage RM;
    Enemy_StatManage EStatM;
    Enemy_Movement EMove;

    float curHp;
    float maxHp;
    // Start is called before the first frame update
    void Start()
    {
        BulletText = GunUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        PM = Pistol.GetComponent<Enemy_GunManage>();
        RM = Rifle.GetComponent<Enemy_GunManage>();
        EStatM = Enemy.GetComponent<Enemy_StatManage>();
        EMove = Enemy.GetComponent<Enemy_Movement>();
        maxHp = EStatM.MaxHp;
        GunUI.SetActive(false);
    }

    void CheckBullets()
    {
        if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.PISTOL)
        {
            BulletText.text = "X " + PM.curAmmunition.ToString();
        }
        else if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            BulletText.text = "X " + RM.curAmmunition.ToString();
        }
    }

    void CheckHp()
    {
        curHp = EStatM.CurHp;
        if (Hpbar != null)
        {
            Hpbar.value = curHp / maxHp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.PISTOL ||
            EMove.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            GunUI.SetActive(true);
            CheckBullets();
        }
    }
}
