using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManage : MonoBehaviour
{
    // Start is called before the first frame update

    public string DELAY = "Delay";
    public string MAGAZINE = "Magainze";
    public string DAMAGE = "Damage";

    public GameObject Player;
    public GameObject WeaponUI;

    public bool AmmunitionZero;
    public bool isShot;

    public float BulletDamage;
    public float ShotDelay = 0.02f;
    public float curAmmunition;
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

            if (curAmmunition == 0)
            {
                AmmunitionZero = true;
            }
        }
        else
            isShot = false;

    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }
}

