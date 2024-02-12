using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StatManage : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyCheckCollision Head;
    public EnemyCheckCollision Body_1;
    public EnemyCheckCollision Body_2;

    public float MaxHp;
    public float CurHp;
    private bool GetHit = false;
    private float Damage = 0;

    public bool isDead;
    void Start()
    {
        Head = GetComponent<EnemyCheckCollision>();
        Body_1 = GetComponent<EnemyCheckCollision>();
        Body_2 = GetComponent<EnemyCheckCollision>();
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
            }
        }
        else
            isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetHit = (Head.isHit || Body_1.isHit || Body_2.isHit);
        HpUpdate();
    }
}
