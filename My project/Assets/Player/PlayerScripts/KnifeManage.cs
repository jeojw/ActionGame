using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManage : MonoBehaviour
{
    public GameObject Player;

    PlayerControl.Weapons curWeapon;
    PolygonCollider2D collider;
    PlayerControl control;
    Animator playerAnim;

    public bool isFence;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        control = Player.GetComponent<PlayerControl>();
        playerAnim = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curWeapon = control.weapon;

        collider.enabled = (curWeapon == PlayerControl.Weapons.KNIFE);
    }
}
