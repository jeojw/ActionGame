using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.XR;

public class Hookg : MonoBehaviour
{
    GrapplingHook grappling;
    Rigidbody2D rigid;
    public DistanceJoint2D joint2D;
    public GameObject Player;
    public GameObject TargetArm;

    // Start is called before the first frame update
    void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<GrapplingHook>();
        rigid = GetComponent<Rigidbody2D>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ring"))
        {
            joint2D.enabled = true;
            grappling.isAttach = true;
        }
    }

}
