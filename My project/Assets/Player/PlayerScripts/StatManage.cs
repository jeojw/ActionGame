using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManage : MonoBehaviour
{
    // Start is called before the first frame update

    public CircleCollider2D HeadHitbox;
    public EdgeCollider2D BodyHitbox_1;
    public EdgeCollider2D BodyHitbox_2;
    public EdgeCollider2D RightLegHitbox_1;
    public EdgeCollider2D RightLegHitbox_2;
    public EdgeCollider2D LeftLegHitbox_1;
    public EdgeCollider2D LeftLegHitbox_2;

    float Damage;

    public enum ALLCONDITIONS
    {
        AWAKE,
        WALK,
        RUN,
        SHOT,
        ROPEFIRE,
        ROPEACTION,
        ROPESHOT,
        GETATTACK,
        DEAD,
        GETITEM
    }

    public enum ATTACKCOEF
    {
        PISTOL = 10,
        KNIFE = 10,
        RIFLE = 10
    }

    public ALLCONDITIONS Condition;
    public float MaxHp;
    public float curHp;

    void Start()
    {
        Condition = ALLCONDITIONS.AWAKE;
        MaxHp = 300f;
        curHp = MaxHp;
    }

    void ATKUpdate()
    {
        if (GetComponent<PlayerControl>().weapon == PlayerControl.Weapons.GUNS)
        {

        }
    }

    void GetAttackUpdate()
    {
        if (HeadHitbox.GetComponent<CheckCollision>().KnifeHit)
        {

        }
    }
    

    void HpUpdate()
    {

    }

    void ConditionUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetAttackUpdate();
        HpUpdate();
        ConditionUpdate();
    }
}
