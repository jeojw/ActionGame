using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_KnifeManage : MonoBehaviour
{
    public GameObject Enemy;
    GameObject Knife;
    PolygonCollider2D PCollider;
    Enemy_Movement EMove;
    Enemy_StatManage EStat;
    Enemy_Movement.ATTACKTYPE ATKType;
    TrailRenderer Trail;

    bool Fence;
    // Start is called before the first frame update
    void Start()
    {
        Trail = transform.GetChild(0).GetComponent<TrailRenderer>();
        EStat = Enemy.GetComponent<Enemy_StatManage>();
        Knife = this.gameObject;
        PCollider = GetComponent<PolygonCollider2D>();
        EMove = Enemy.GetComponent<Enemy_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        ATKType = EMove.AttackType;
        
        PCollider.enabled = ATKType == Enemy_Movement.ATTACKTYPE.SWORD && 
                            EMove.isFencing;
        Trail.enabled = PCollider.enabled;
        if (EStat.isDead)
            Knife.SetActive(false);
    }
}
