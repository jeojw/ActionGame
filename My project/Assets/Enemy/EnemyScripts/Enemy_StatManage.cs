using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StatManage : MonoBehaviour
{
    // Start is called before the first frame update

    EnemyCheckCollision HitCheck;

    public float MaxHp;
    public float CurHp;

    private bool _GetHit = false;
    public bool GetHit { 
        get { return _GetHit; }
        set { _GetHit = value; }
    }

    private float Damage = 0;
    private bool _isDrop = false;
    public bool isDrop { 
        get { return _isDrop; }
        set { _isDrop = value; }
    }
    private bool _isDead;
    public bool isDead {
        get { return _isDead; }
        set { _isDead = value; }
    }
    void Start()
    {
        CurHp = MaxHp;
        HitCheck = GetComponent<EnemyCheckCollision>();
    }

    public void SetDamage(float _damage)
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
        _GetHit = HitCheck.isHit;

        if (CurHp > 0)
        {
            if (Damage != 0)
            {
                CurHp -= Damage;
                ResetDamage();
            }
            
        }
        else
        {
            _isDead = true;
            _isDrop = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        HpUpdate();
    }
}
