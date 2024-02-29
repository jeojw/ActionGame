using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.XR;

public class Hookg : MonoBehaviour
{
    CircleCollider2D CC;
    GrapplingHook grappling;
    Rigidbody2D rigid;
    public DistanceJoint2D joint2D;
    public GameObject Player;
    public GameObject TargetArm;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CircleCollider2D>();  
        grappling = GameObject.Find("Player").GetComponent<GrapplingHook>();
        rigid = GetComponent<Rigidbody2D>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void Circlecast()
    {
        RaycastHit2D Hit = Physics2D.CircleCast(transform.position, CC.radius, (joint2D.connectedAnchor - joint2D.anchor).normalized, 0.01f, LayerMask.GetMask("Ring"));

        joint2D.enabled = Hit.collider != null;
        grappling.isAttach = Hit.collider != null;
    }

    private void Update()
    {
        Circlecast();
    }

}
