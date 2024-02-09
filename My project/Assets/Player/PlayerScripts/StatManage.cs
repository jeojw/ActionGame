using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManage : MonoBehaviour
{
    // Start is called before the first frame update

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
    private float MaxHp;
    private float curHp;

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

    void HpUpdate()
    {

    }

    void ConditionUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
