using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StatManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Head;
    public GameObject Body;
    public GameObject Left_Leg_1;
    public GameObject Left_Leg_2;
    public GameObject Right_Leg_1;
    public GameObject Right_Leg_2;

    EnemyCheckCollision Head_Hit;
    EnemyCheckCollision Body_Hit;
    EnemyCheckCollision LeftLeg_1_Hit;
    EnemyCheckCollision RightLeg_1_Hit;
    EnemyCheckCollision LeftLeg_2_Hit;
    EnemyCheckCollision RightLeg_2_Hit;

    public float MaxHp;
    public float CurHp;

    public bool GetHit = false;
    private bool KnifeHit = false;
    private bool RifleHit = false;
    private bool PistolHit = false;
    private bool PisteHit = false;
    private bool zeroPhysic = false;

    private float Damage = 0;
    public bool isDrop = false;

    public bool isDead;
    void Start()
    {
        Head_Hit = Head.GetComponent<EnemyCheckCollision>();
        Body_Hit = Body.GetComponent<EnemyCheckCollision>();
        LeftLeg_1_Hit = Left_Leg_1.GetComponent<EnemyCheckCollision>();
        LeftLeg_2_Hit = Left_Leg_2.GetComponent<EnemyCheckCollision>();
        RightLeg_1_Hit = Right_Leg_1.GetComponent<EnemyCheckCollision>();
        RightLeg_2_Hit = Right_Leg_2.GetComponent<EnemyCheckCollision>();
        CurHp = MaxHp;
    }

    void SetDamage(float _damage)
    {
        Damage = _damage;
    }

    void ResetDamage()
    {
        Damage = 0;
    }

    public void SetHp(float _hp)
    {
        MaxHp = _hp;
    }

    void HpUpdate()
    {
        GetHit = (Head_Hit.isHit || Body_Hit.isHit ||
                  LeftLeg_1_Hit.isHit || LeftLeg_2_Hit.isHit ||
                  RightLeg_1_Hit.isHit || RightLeg_1_Hit.isHit);
        KnifeHit = (Body_Hit.KnifeHit ||
                    LeftLeg_1_Hit.KnifeHit || LeftLeg_2_Hit.KnifeHit ||
                    RightLeg_1_Hit.KnifeHit || RightLeg_2_Hit.KnifeHit);
        PistolHit = (Head_Hit.PistolBulletHit || Body_Hit.PistolBulletHit || 
                     LeftLeg_1_Hit.PistolBulletHit || LeftLeg_2_Hit.PistolBulletHit ||
                     RightLeg_1_Hit.PistolBulletHit || RightLeg_2_Hit.PistolBulletHit);
        RifleHit = (Head_Hit.RifleBulletHit || Body_Hit.RifleBulletHit ||  
                    LeftLeg_1_Hit.RifleBulletHit || LeftLeg_2_Hit.RifleBulletHit || 
                    RightLeg_1_Hit.RifleBulletHit || RightLeg_2_Hit.RifleBulletHit);
        PisteHit = (Head_Hit.PisteHit || Body_Hit.PisteHit || 
                    LeftLeg_1_Hit.PisteHit || LeftLeg_2_Hit.PisteHit || 
                    RightLeg_1_Hit.PisteHit || RightLeg_2_Hit.PisteHit);

        if (CurHp > 0)
        {
            if (PisteHit)
            {
                SetDamage(30f);
                PisteHit = false;
            }
            if (RifleHit)
            {
                SetDamage(150f);
                RifleHit = false;
            } 
            if (PistolHit)
            {
                SetDamage(75f);
                PistolHit = false;
            }
            if (KnifeHit)
            {
                SetDamage(75f);
                KnifeHit = false;
            } 
            if (Damage != 0)
            {
                CurHp -= Damage;
                ResetDamage();
            }
            
        }
        else
        {
            isDead = true;
            isDrop = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        HpUpdate();
    }
}
