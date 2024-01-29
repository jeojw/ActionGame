using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManage : MonoBehaviour
{
    // Start is called before the first frame update
    public enum AllConditions
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

    public AllConditions Condition;
    void Start()
    {
        Condition = AllConditions.AWAKE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
