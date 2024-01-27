using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public GameObject Target;
    BoxCollider2D Collid;
    // Start is called before the first frame update
    void Start()
    {
        Collid = Target.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
