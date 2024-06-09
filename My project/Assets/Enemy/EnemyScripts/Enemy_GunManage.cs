using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GunManage : MonoBehaviour
{
    public GameObject Enemy;
    public Transform FirePos;

    ParticleSystem PS;

    Enemy_Movement EMove;
    public float ShotDelay;
    public float curAmmunition;
    private float maxAmmunition;
    public bool isZero;
    // Start is called before the first frame update
    void Start()
    {
        PS = transform.GetChild(0).GetComponent<ParticleSystem>();
        EMove = Enemy.GetComponent<Enemy_Movement>();
        if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.PISTOL)
        {
            ShotDelay = 1f;
            maxAmmunition = 6;
        }
        else if (EMove.AttackType == Enemy_Movement.ATTACKTYPE.RIFLE)
        {
            ShotDelay = 0.5f;
            maxAmmunition = 15;
        }
        curAmmunition = maxAmmunition;
        isZero = false;
    }

    public void Shot()
    {
        if (curAmmunition != 0)
        {
            curAmmunition--;
        }
        if (curAmmunition == 0)
        {
            isZero = true;
        }
    }

   public void Effect()
    {

        PS.Emit(100);
        PS.Play();
    }

    public void EffectStop()
    {
        PS.Stop();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
