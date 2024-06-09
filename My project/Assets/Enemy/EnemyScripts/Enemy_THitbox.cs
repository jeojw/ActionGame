using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_THitbox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;

    Enemy_StatManage EStat;
    CapsuleCollider2D CC;
    void Start()
    {
        CC = GetComponent<CapsuleCollider2D>();
        EStat = Enemy.GetComponent<Enemy_StatManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EStat.isDead)
            CC.enabled = false;
    }
}
