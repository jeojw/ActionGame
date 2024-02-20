using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sound_Manage : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Knife;

    public AudioSource Walk;

    public AudioSource PistolShot;
    public AudioSource RifleShot;

    Enemy_Movement EMove;
    Enemy_StatManage EStat;

    float moveCool;
    float moveCoef = 2;

    bool isShot;
    bool isFence;

    bool isWalk;
    // Start is called before the first frame update
    void Start()
    {
        EMove = GetComponent<Enemy_Movement>();
        EStat = GetComponent<Enemy_StatManage>();
    }

    void MoveSoundUpdate()
    {
        isWalk = EMove.isWalking;

        moveCool += Time.deltaTime * moveCoef;

        if (moveCool >= 1.0f)
        {
            moveCool = 0;
            if (isWalk)
            {
                Walk.Play();
                moveCoef = 2;
            }
        }
    }

    void WeaponSoundUpdate()
    {
        isShot = EMove.isShooting;
        if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.PISTOL)
        {
            if (isShot)
            {
                PistolShot.Play();
            }
        }
        else if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            if (isShot)
            {
                RifleShot.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!EStat.isDead)
        {
            MoveSoundUpdate();
            WeaponSoundUpdate();
        }

    }
}
