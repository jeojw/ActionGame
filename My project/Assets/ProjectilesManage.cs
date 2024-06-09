using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilesManage : MonoBehaviour
{
    public GameObject Player;
    public GameObject Pistol;
    public GameObject Rifle;

    List<GameObject> BulletList;

    GameObject Bullets;
    GameObject CurBullet;
    GameObject EnemyCurBullet;

    PlayerControl playControl;
    SetGame SG;

    private bool isShot = false;

    // Start is called before the first frame update
    void Start()
    {
        BulletList = new List<GameObject>();
        Bullets = Resources.Load<GameObject>("Prefabs/BulletObjects");

        playControl = Player.GetComponent<PlayerControl>();
        SG = GetComponent<SetGame>();
    }

    public void ResetList()
    {
        for (int i = 0; i < BulletList.Count; i++)
            Destroy(BulletList[i]);
        BulletList.Clear();
    }

    void Player_Bullet_Shot()
    {
        if (playControl.isShooting &&
            playControl.ShotDelayElapsed == 0)
        {
            if (playControl.weapon == PlayerControl.Weapons.PISTOL && !Pistol.GetComponent<PistolManage>().AmmunitionZero)
            {
                CurBullet = Instantiate(Bullets.transform.GetChild(0).gameObject);
                BulletList.Add(CurBullet);
            }
            else if(playControl.weapon == PlayerControl.Weapons.RIFLE && !Rifle.GetComponent<RifleManage>().AmmunitionZero)
            {
                CurBullet = Instantiate(Bullets.transform.GetChild(1).gameObject);
                BulletList.Add(CurBullet);
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
                    EnemyCurBullet = Instantiate(Bullets.transform.GetChild(2).gameObject, SG.EnemyList[i].transform.GetChild(2).GetChild(1).GetChild(0).transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                    EnemyCurBullet.GetComponent<Enemy_Bullet_Manage>().SetDirect((int)SG.EnemyMovementList[i].detectDirection);
                    BulletList.Add(EnemyCurBullet);
                }
                if (SG.EnemyMovementList[i].AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
                {
                    EnemyCurBullet = Instantiate(Bullets.transform.GetChild(2).gameObject, SG.EnemyList[i].transform.GetChild(2).GetChild(2).GetChild(0).transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                    EnemyCurBullet.GetComponent<Enemy_Bullet_Manage>().SetDirect((int)SG.EnemyMovementList[i].detectDirection);
                    BulletList.Add(EnemyCurBullet);
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
