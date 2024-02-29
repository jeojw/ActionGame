using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;

    PlayerControl playerControl;
    PlayerControl.Weapons curWeapon;
    PolygonCollider2D Collider;
    void Start()
    {
        Collider = GetComponent<PolygonCollider2D>();
        playerControl = Player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        curWeapon = playerControl.weapon;
        Collider.enabled = (curWeapon == PlayerControl.Weapons.NONE &&
                            playerControl.isAttack);
    }
}
