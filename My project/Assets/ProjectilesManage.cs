using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilesManage : MonoBehaviour
{
    public GameObject Player;
    public GameObject Weapon;

    GameObject Bullets;
    GameObject CurBullet;

    PlayerControl playControl;

    private bool isShot = false;

    // Start is called before the first frame update
    void Start()
    {
        Bullets = Resources.Load<GameObject>("Prefabs/BulletObjects");
        playControl = Player.GetComponent<PlayerControl>();
    }

    void Player_Bullet_Shot()
    {
        if (playControl.isShooting &&
            playControl.ShotDelayElapsed == 0)
        {
            if (!Weapon.GetComponent<GunManage>().AmmunitionZero)
            {
                CurBullet = Instantiate(Bullets.transform.GetChild(0).gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Player_Bullet_Shot();
    }
}
