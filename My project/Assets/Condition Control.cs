using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionControl : MonoBehaviour
{
    GameObject Player = GameObject.Find("Player");


    public string condition = "Awake";
    public bool isWalking = false;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isShooting = false;
    // Start is called before the first frame update
    void Gun_Shot()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isShooting = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
