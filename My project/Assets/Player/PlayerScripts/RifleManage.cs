using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManage : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public GameObject WeaponUI;

    public bool AmmunitionZero = true;
    public bool isShot;
    public bool isReload;

    public float BulletDamage;
    public float ShotDelay = 0.01f;
    public float curAmmunition = 0;
    public float maxAmmunition;

    PlayerControl playControl;
    void Start()
    {
        curAmmunition = 0;
        AmmunitionZero = true;
        playControl = Player.GetComponent<PlayerControl>();
    }

    public void ResetMagazine()
    {
        AmmunitionZero = false;
        curAmmunition = maxAmmunition;
    }

    void Shot()
    {
        if (playControl.ShotDelayElapsed == 0 &&
            playControl.isShooting &&
            playControl.weapon == PlayerControl.Weapons.RIFLE)
        {
            isShot = true;
            if (curAmmunition != 0)
            {
                curAmmunition -= 1;
            }
        }
        else
            isShot = false;

    }

    void Reload()
    {
        if (playControl.isGetItem && playControl.GetItemType == ItemManage.ITEMTYPE.RIFLE)
        {
            Debug.Log(true);
            isReload = true;
        }
        else
            isReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (curAmmunition <= 0)
            AmmunitionZero = true;
        Shot();
        Reload();
    }
}

