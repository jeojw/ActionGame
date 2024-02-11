using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void SetPhysics(Vector2 velocity)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
