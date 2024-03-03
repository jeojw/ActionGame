using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hand;
    public LineRenderer line;
    public GameObject hook;
    public Transform waist;
    Rigidbody2D hookRigid;
    Rigidbody2D playerRigid;
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
    private float gravity = 0;

    private bool isPulling = false;

    public PlayerControl.Weapons CurWeapon;
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 3;
        line.endWidth = line.startWidth = 0.5f;
        line.SetPosition(0, waist.transform.position);
        line.SetPosition(1, hand.transform.position);
        line.SetPosition(2, hook.transform.position);
        line.useWorldSpace = true;
        isAttach = false;
        RopeDelayElapsed = 0;
        hook.gameObject.SetActive(false);
        hookRigid = hook.GetComponent<Rigidbody2D>();
        playerRigid = GetComponent<Rigidbody2D>();
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
        
        if (Vector2.Distance(hook.transform.position, hand.transform.position) <= 3f ||
            hook.transform.position.y > hand.transform.position.y)
        {
            gravity += hookRigid.gravityScale * Time.deltaTime * 10;
            hook.transform.position = hand.transform.position + new Vector3(Mathf.Cos(RotateAngle), Mathf.Sin(RotateAngle) - gravity, 1);
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
                if (playControl.isGround && !playControl.isJumpStart && !playControl.isLand)
                {
                    Ready_To_Throw_Stand();
                    gravity = 0;
                }
                if (!playControl.isGround || playControl.isJumpStart)
                {
                    Ready_To_Throw_OnAir();
                }
                    
            }
                

            line.SetPosition(0, waist.transform.position);
            line.SetPosition(1, hand.transform.position);
            line.SetPosition(2, hook.transform.position);

            if (Input.GetKeyDown(KeyCode.E) && RopeDelayElapsed == 0)
            {
                isHookThrow = true;
                hook.transform.position = hand.transform.position;
                mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - hand.transform.position;
                isHookActive = true;
                isLineMax = false;
            }

            if (isHookActive && !isLineMax && !isAttach)
            {
                hook.transform.Translate(mousedir.normalized * Time.deltaTime * 30);

                if (Vector2.Distance(hand.transform.position, hook.transform.position) > MaxLine)
                {
                    isLineMax = true;
                }
            }
            else if (isHookActive && isLineMax && !isAttach)
            {
                hook.transform.position = Vector2.MoveTowards(hook.transform.position, hand.transform.position, Time.deltaTime * 30);
                if (Vector2.Distance(hand.transform.position, hook.transform.position) < 0.1f)
                {
                    isLineMax = false;
                    isHookActive = false;
                    isHookThrow = false;
                }
            }
            else if (isAttach)
            {
                
                if (Input.GetKeyDown(KeyCode.E) && !isPulling)
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
                    isPulling = true;
                    playerRigid.AddForce((hook.transform.position - hand.transform.position).normalized * 250f, ForceMode2D.Impulse);
                    
                }
                
            }
            if (playerRigid.velocity.y <= 0 && isPulling)
            {
                isPulling = false;
                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                isHookThrow = false;
                hookg.joint2D.enabled = false;
                hook.gameObject.SetActive(false);
            }
        }
        else
            hook.gameObject.SetActive(false);
    }
}
