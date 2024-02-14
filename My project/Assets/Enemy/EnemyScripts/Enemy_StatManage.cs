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
    private float Damage = 0;

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
        if (CurHp > 0)
        {
            if (GetHit)
            {
                SetDamage(10f);
                CurHp -= Damage;
                ResetDamage();
                GetHit = false;
            }
        }
        else
            isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetHit = (Head_Hit.isHit || Body_1_Hit.isHit || Body_2_Hit.isHit);
        HpUpdate();
    }
}
