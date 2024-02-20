using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GunManage : MonoBehaviour
{
    public GameObject Enemy;

    Enemy_Movement EMove;
    public float ShotDelay;
    public float curAmmunition;
    private float maxAmmunition;
    // Start is called before the first frame update
    void Start()
    {
        EMove = Enemy.GetComponent<Enemy_Movement>();
        if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.PISTOL)
        {
            ShotDelay = 1f;
            maxAmmunition = 6;
        }
        else if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            ShotDelay = 0.5f;
            maxAmmunition = 30;
        }
        curAmmunition = maxAmmunition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
