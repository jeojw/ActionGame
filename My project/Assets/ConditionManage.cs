using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManage : MonoBehaviour
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

    public ALLCONDITIONS Condition;
    private float MaxHp;
    private float curHp;

    void Start()
    {
        Condition = ALLCONDITIONS.AWAKE;
        MaxHp = 300f;
        curHp = MaxHp;
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
