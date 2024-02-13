using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManage : MonoBehaviour
{
    public GameObject Player;

    PlayerControl.Weapons curWeapon;
    PolygonCollider2D collider;
    PlayerControl control;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        control = Player.GetComponent<PlayerControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        curWeapon = control.weapon;

        if (curWeapon == PlayerControl.Weapons.KNIFE)
        {
            collider.enabled = true;
        }
        else
            collider.enabled = false;
    }
}
