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

    PlayerControl playControl;

    float ATK;

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
        playControl = GetComponent<PlayerControl>();
        MaxHp = 300f;
        curHp = MaxHp;

        playControl = GetComponent<PlayerControl>();
        isDead = false;
    }

    void ATKUpdate()
    {
        if (playControl.weapon == PlayerControl.Weapons.GUNS)
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
        if (curHp >= 0)
        {

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
        GetAttackUpdate();
        HpUpdate();
        ConditionUpdate();
    }
}
