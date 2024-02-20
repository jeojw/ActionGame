using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilesManage : MonoBehaviour
{
    public GameObject Player;
    public GameObject Weapon;

    SetGame SG;

    GameObject Bullets;
    GameObject CurBullet;
    GameObject EnemyCurBullet;

    PlayerControl playControl;

    private bool isShot = false;

    // Start is called before the first frame update
    void Start()
    {
        Bullets = Resources.Load<GameObject>("Prefabs/BulletObjects");
        playControl = Player.GetComponent<PlayerControl>();
        SG = GetComponent<SetGame>();
    }

    void Player_Bullet_Shot()
    {
        if (playControl.isShooting &&
            playControl.ShotDelayElapsed == 0)
        {
            if (playControl.weapon == PlayerControl.Weapons.PISTOL && !Weapon.GetComponent<PistolManage>().AmmunitionZero)
            {
                CurBullet = Instantiate(Bullets.transform.GetChild(0).gameObject);
            }
            else if(playControl.weapon == PlayerControl.Weapons.RIFLE && !Weapon.GetComponent<PistolManage>().AmmunitionZero)
            {
                CurBullet = Instantiate(Bullets.transform.GetChild(1).gameObject);
            }
        }
    }

    void Enemy_Bullet_Shot()
    {
        for (int i = 0; i < SG.EnemyMovementList.Count; i++)
        {
            if (SG.EnemyMovementList[i].isShooting &&
                SG.EnemyMovementList[i].AttackCoolElapsed == 0)
            {
                if (SG.EnemyMovementList[i].AttackType == Enemy_Movement.ATTACKTYPE.PISTOL)
                {
                    EnemyCurBullet = Instantiate(Bullets.transform.GetChild(2).gameObject);
                }
                if (SG.EnemyMovementList[i].AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
                {
                    EnemyCurBullet = Instantiate(Bullets.transform.GetChild(3).gameObject);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Player_Bullet_Shot();
        Enemy_Bullet_Shot();
    }
}
