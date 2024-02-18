using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StatManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Head;
    public GameObject Body_1;
    public GameObject Body_2;

    EnemyCheckCollision Head_Hit;
    EnemyCheckCollision Body_1_Hit;
    EnemyCheckCollision Body_2_Hit;

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
        Body_1_Hit = Body_1.GetComponent<EnemyCheckCollision>();
        Body_2_Hit = Body_2.GetComponent<EnemyCheckCollision>();
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
        GetHit = (Head_Hit.isHit || Body_1_Hit.isHit || Body_2_Hit.isHit);
        KnifeHit = (Head_Hit.KnifeHit || Body_1_Hit.KnifeHit || Body_2_Hit.KnifeHit);
        PistolHit = (Head_Hit.PistolBulletHit || Body_1_Hit.PistolBulletHit || Body_2_Hit.PistolBulletHit);
        RifleHit = (Head_Hit.RifleBulletHit || Body_1_Hit.RifleBulletHit || Body_2_Hit.RifleBulletHit);
        PisteHit = (Head_Hit.PisteHit || Body_1_Hit.PisteHit || Body_2_Hit.PisteHit);

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
            CurHp -= Damage;
            ResetDamage();
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
