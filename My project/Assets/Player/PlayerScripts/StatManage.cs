using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HeadHitbox;
    public GameObject BodyHitbox_1;
    public GameObject BodyHitbox_2;
    public GameObject RightLegHitbox_1;
    public GameObject RightLegHitbox_2;
    public GameObject LeftLegHitbox_1;
    public GameObject LeftLegHitbox_2;

    PlayerCheckCollision HeadCheck;
    PlayerCheckCollision BodyCheck_1;
    PlayerCheckCollision BodyCheck_2;
    PlayerCheckCollision RightLegCheck_1;
    PlayerCheckCollision RightLegCheck_2;
    PlayerCheckCollision LeftLegCheck_1;
    PlayerCheckCollision LeftLegCheck_2;


    PlayerControl playControl;

    float ATK;
    float Damage = 0;
    bool GetHit = false;

    public bool isDead;

    public enum ATTACKCOEF
    {
        PISTOL = 10,
        KNIFE = 10,
        RIFLE = 10
    }

    public float MaxHp;
    public float curHp;

    void Start()
    {
        HeadCheck = HeadHitbox.GetComponent<PlayerCheckCollision>();
        BodyCheck_1 = BodyHitbox_1.GetComponent<PlayerCheckCollision>();
        BodyCheck_2 = BodyHitbox_2.GetComponent<PlayerCheckCollision>();
        RightLegCheck_1 = RightLegHitbox_1.GetComponent<PlayerCheckCollision>();
        RightLegCheck_2 = RightLegHitbox_2.GetComponent<PlayerCheckCollision>();
        LeftLegCheck_1 = LeftLegHitbox_1.GetComponent<PlayerCheckCollision>();
        LeftLegCheck_2 = LeftLegHitbox_2.GetComponent<PlayerCheckCollision>();

        playControl = GetComponent<PlayerControl>();
        curHp = MaxHp;

        isDead = false;
    }

    void ATKUpdate()
    {
        if (playControl.weapon == PlayerControl.Weapons.GUNS)
        {

        }
    }

    public void SetHp(float _hp)
    {
        MaxHp = _hp;
    }
    void SetGetDamage(float _damage)
    {
        Damage = _damage;
    }

    void ResetDamage()
    {
        Damage = 0;
    }

    void GetAttackUpdate()
    {
           
    }

    void HpUpdate()
    {
        if (curHp > 0)
        {
            if (GetHit)
            {
                SetGetDamage(70f);
            }
            if (Damage != 0)
            {
                curHp -= Damage;
                ResetDamage();
            }

            if (playControl.GetItem)
            {

            }
        }
        else
            isDead = true;
    }

    void ConditionUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetHit = (HeadCheck.isHit || BodyCheck_1.isHit || BodyCheck_2.isHit ||
                  RightLegCheck_1.isHit || RightLegCheck_2.isHit ||
                  LeftLegCheck_1.isHit || LeftLegCheck_2.isHit);
        GetAttackUpdate();
        HpUpdate();
        ConditionUpdate();
     }
}
