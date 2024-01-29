using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.XR;

public class Hookg : MonoBehaviour
{
    GrapplingHook grappling;
    public DistanceJoint2D joint2D;
    public GameObject hookObject;
    SpriteRenderer hookSprite;
    Transform hookSpriteRotate;

    // Start is called before the first frame update
    void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<GrapplingHook>();
        joint2D = GetComponent<DistanceJoint2D>();
        hookSprite = hookObject.GetComponent<SpriteRenderer>();
        hookSpriteRotate = hookObject.GetComponent<Transform>();
        joint2D.maxDistanceOnly = true;
        joint2D.distance = grappling.MaxLine;
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

    void Update()
    {
        if (grappling.isHookActive || grappling.isLineMax)
        {
            hookSpriteRotate.rotation = Quaternion.Euler(0, 0, -Vector3.Angle(Camera.main.ScreenToWorldPoint(Input.mousePosition), grappling.line.GetPosition(0)));
            hookSprite.enabled = true;
        }
           
    }
}
