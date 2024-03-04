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
        Head_Hit = Head.GetComponent<EnemyCheckCollision>();
        Body_Hit = Body.GetComponent<EnemyCheckCollision>();
        LeftLeg_1_Hit = Left_Leg_1.GetComponent<EnemyCheckCollision>();
        LeftLeg_2_Hit = Left_Leg_2.GetComponent<EnemyCheckCollision>();
        RightLeg_1_Hit = Right_Leg_1.GetComponent<EnemyCheckCollision>();
        RightLeg_2_Hit = Right_Leg_2.GetComponent<EnemyCheckCollision>();
        CurHp = MaxHp;
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
        _GetHit = (Head_Hit.isHit || Body_Hit.isHit ||
                  LeftLeg_1_Hit.isHit || LeftLeg_2_Hit.isHit ||
                  RightLeg_1_Hit.isHit || RightLeg_2_Hit.isHit);

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
