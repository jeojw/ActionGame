using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UIManage : MonoBehaviour
{
    public Slider Hpbar;
    public GameObject Enemy;
    Enemy_StatManage EStatM;

    float curHp;
    float maxHp;
    // Start is called before the first frame update
    void Start()
    {
        EStatM = Enemy.GetComponent<Enemy_StatManage>();
        maxHp = EStatM.MaxHp;
        
    }

    void CheckHp()
    {
        curHp = EStatM.CurHp;
        if (Hpbar != null)
        {
            Hpbar.value = curHp / maxHp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
    }
}
