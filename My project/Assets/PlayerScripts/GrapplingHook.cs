using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hand;
    public LineRenderer line;
    public Transform hook;
    Vector2 mousedir;

    public float RopeDelay;
    private float RopeDelayStart = 0;
    public float RopeDelayElapsed = 0;

    public bool isHookActive;
    public bool isHookThrow;
    public bool isLineMax;
    public bool isAttach;

    public float MaxLine;

    private float RotateAngle;

    public PlayerControl.Weapons CurWeapon;
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 3;
        line.endWidth = line.startWidth = 0.5f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hand.transform.position);
        line.SetPosition(2, hook.position);
        line.useWorldSpace = true;
        isAttach = false;
        RopeDelayElapsed = 0;
        hook.gameObject.SetActive(false);
    }
    
    void Ready_To_Throw()
    {
        if (GetComponent<PlayerControl>().isGround)
        {
            RotateAngle += 20 * Time.deltaTime;
            hook.transform.position = hand.transform.position + new Vector3(Mathf.Cos(RotateAngle), Mathf.Sin(RotateAngle), 1) * 2f;
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hand.transform.position);
        line.SetPosition(2, hook.position);
    }
    // Update is called once per frame
    void Update()
    {
        CurWeapon = GetComponent<PlayerControl>().weapon;
        if (CurWeapon == PlayerControl.Weapons.ROPE)
        {
            hook.gameObject.SetActive(true);

            if (isHookActive)
            {
                RopeDelayStart = Time.time;
            }
            else
            {
                RopeDelayElapsed = Time.time - RopeDelayStart;
                if (RopeDelayElapsed >= RopeDelay)
                {
                    RopeDelayElapsed = 0;
                    RopeDelayStart = 0;
                }
            }
            if (!isHookThrow)
                Ready_To_Throw();

            line.SetPosition(0, transform.position);
            line.SetPosition(1, hand.transform.position);
            line.SetPosition(2, hook.position);

            if (Input.GetKeyDown(KeyCode.E) && RopeDelayElapsed == 0)
            {
                isHookThrow = true;
                hook.position = hand.transform.position;
                mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - hand.transform.position;
                isHookActive = true;
                isLineMax = false;
                //hook.gameObject.SetActive(true);
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
                    isLineMax = false;
                    isHookActive = false;
                    isHookThrow = false;
                    //hook.gameObject.SetActive(false);
                }
            }
            else if (isAttach)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isAttach = false;
                    isHookActive = false;
                    isLineMax = false;
                    isHookThrow = false;
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
        else
            hook.gameObject.SetActive(false);

        Debug.Log(RopeDelayElapsed);
    }
}
