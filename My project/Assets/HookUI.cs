using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookUI : MonoBehaviour
{
    public GameObject Player;
    PlayerControl playerControl;

    SpriteRenderer spriteRenderer;
    Vector3 mousePos;
    float Angle;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerControl = Player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.weapon == PlayerControl.Weapons.ROPE)
        {
            spriteRenderer.enabled = true;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Angle = Mathf.Atan2(transform.position.y - Player.transform.position.y, transform.position.x - Player.transform.position.x) * Mathf.Rad2Deg;
            transform.position = new Vector2(mousePos.x, mousePos.y);
            transform.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
        }
        else
            spriteRenderer.enabled = false;
    }
}
