using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManage : MonoBehaviour
{
    public GameObject Player;

    PlayerControl.Weapons curWeapon;
    PolygonCollider2D Pcollider;
    PlayerControl control;
    Animator playerAnim;

    public bool isFence;
    // Start is called before the first frame update
    void Start()
    {
        Pcollider = GetComponent<PolygonCollider2D>();
        control = Player.GetComponent<PlayerControl>();
        playerAnim = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curWeapon = control.weapon;

        Pcollider.enabled = curWeapon == PlayerControl.Weapons.KNIFE &&
                            playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f && (
                            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_1") ||
                            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_2") ||
                            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_3"));
    }
}
