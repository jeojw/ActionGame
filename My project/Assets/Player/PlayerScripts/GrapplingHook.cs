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
    public Transform waist;
    Rigidbody2D hookRigid;
    Rigidbody2D Rigid;
    Hookg hookg;
    PlayerControl playControl;
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
        line.SetPosition(0, waist.transform.position);
        line.SetPosition(1, hand.transform.position);
        line.SetPosition(2, hook.position);
        line.useWorldSpace = true;
        isAttach = false;
        RopeDelayElapsed = 0;
        hook.gameObject.SetActive(false);

        hookRigid = hook.GetComponent<Rigidbody2D>();
        Rigid = GetComponent<Rigidbody2D>();
        hookg = hook.GetComponent<Hookg>();
        playControl = GetComponent<PlayerControl>();
    }
    
    void Ready_To_Throw_Stand()
    {
        RotateAngle += 20 * Time.deltaTime;
        hook.transform.position = hand.transform.position + new Vector3(Mathf.Cos(RotateAngle), Mathf.Sin(RotateAngle), 1) * 2f;
    }

    void Ready_To_Throw_OnAir()
    {
        if (hook.transform.position.y > hand.transform.position.y) {
            if (Vector2.Distance(hook.transform.position, hand.transform.position) <= 3f)
                hookRigid.velocity = new Vector3(0, hookRigid.gravityScale * Time.deltaTime, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        CurWeapon = playControl.weapon;
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
            {
                if (playControl.isGround)
                    Ready_To_Throw_Stand();
                else if (!playControl.isGround || playControl.isJumpStart)
                    Ready_To_Throw_OnAir();
            }
                

            line.SetPosition(0, waist.transform.position);
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
                    hookg.joint2D.enabled = false;
                    hook.gameObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    isLineMax = false;
                    Rigid.AddForce((hook.position - hand.transform.position).normalized * 30f, ForceMode2D.Impulse);
                }
            }
        }
        else
            hook.gameObject.SetActive(false);
    }
}