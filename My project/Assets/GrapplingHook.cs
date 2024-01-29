using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hand;
    public LineRenderer line;
    public Transform hook;
    Vector2 mousedir;

    public bool isHookActive;
    public bool isLineMax;
    public bool isAttach;

    public float MaxLine;

    public Movement.Weapons CurWeapon;
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.5f;
        line.SetPosition(0, hand.transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
        isAttach = false;
        hook.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CurWeapon = GetComponent<Movement>().weapon;
        if (CurWeapon == Movement.Weapons.ROPE)
        {
            line.SetPosition(0, hand.transform.position);
            line.SetPosition(1, hook.position);

            if (Input.GetKeyDown(KeyCode.E))
            {
                hook.position = hand.transform.position;
                mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - hand.transform.position;
                isHookActive = true;
                isLineMax = false;
                hook.gameObject.SetActive(true);
            }

            if (isHookActive && !isLineMax && !isAttach)
            {
                hook.Translate(mousedir.normalized * Time.deltaTime * 30);

                if (Vector2.Distance(hand.transform.position, hook.position) > MaxLine)
                {
                    isLineMax = true;
                }
            }
            else if (isHookActive && isLineMax && !isAttach)
            {
                hook.position = Vector2.MoveTowards(hook.position, hand.transform.position, Time.deltaTime * 30);
                if (Vector2.Distance(hand.transform.position, hook.position) < 0.1f)
                {
                    isHookActive = false;
                    isLineMax = false;
                    hook.gameObject.SetActive(false);
                }
            }
            else if (isAttach)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isAttach = false;
                    isHookActive = false;
                    isLineMax = false;
                    hook.GetComponent<Hookg>().joint2D.enabled = false;
                    hook.gameObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    isLineMax = false;
                    GetComponent<Rigidbody2D>().AddForce((hook.position - hand.transform.position).normalized * 30f, ForceMode2D.Impulse);
                }
            }
        }
    }
}
