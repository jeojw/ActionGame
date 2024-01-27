using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static GameObject instance;
    public 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Player : MonoBehaviour
{
    public new SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        renderer.color = new Color(0, 0, 0);
    }
    void Update()
    {
        renderer.enabled = false;   
    }
}
