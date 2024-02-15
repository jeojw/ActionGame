using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_KnifeManage : MonoBehaviour
{
    public GameObject Enemy;
    GameObject Knife;
    PolygonCollider2D Collider;
    Enemy_Movement EMove;
    Enemy_StatManage EStat;

    bool Fence;
    // Start is called before the first frame update
    void Start()
    {
        EStat = Enemy.GetComponent<Enemy_StatManage>();
        Knife = this.gameObject;
        Collider = GetComponent<PolygonCollider2D>();
        EMove = Enemy.GetComponent<Enemy_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider.enabled = EMove.isFencing;
        if (EStat.isDead)
            Knife.SetActive(false);
    }
}
