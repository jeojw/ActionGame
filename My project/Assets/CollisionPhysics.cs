using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rigid;
    public Vector2 PhysicsVelocity;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void SetPhysics(Vector2 velocity)
    {
        PhysicsVelocity = velocity;
    }

    public void PhysicsUpdate()
    {
        rigid.AddForce(PhysicsVelocity, ForceMode2D.Impulse);
        PhysicsVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsUpdate();
    }
}
