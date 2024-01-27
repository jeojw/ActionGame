using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    GrapplingHook grappling;
    Animator anim;

    public Vector2 movement = Vector2.zero;

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public enum Weapons
    {
        GUNS,
        KNIFE,
        ROPE
    }

    private int jumpCount = 1;
    public float speed = 0.02f;
    public float jumpV = 2f;
    private int weaponPos = 0;

    private float KeyPressTime = 0;
    private float PerKeyPressTime = 0;
    private float KeyPressInterval = 0.5f;
    public float RunningInterval = 0.5f;

    public Direction direction = Direction.RIGHT;
    public Weapons weapon;

    public bool isShooting = false; 
    public bool isReloading = false;
    public bool isFencing = false;
    public bool isGrapplingShot = false;

    public bool isJumpStart = false;
    public bool isJump = false;
    public bool isLanding = false;
    public bool isRolling = false;
    public bool isAttack = false;
    public bool isWalking = false;
    public bool isGround = false;
    public bool isRunning = false;

    public bool IsDoublePressed = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        grappling = GetComponent<GrapplingHook>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
            isJump = true;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            isJump = false;
            jumpCount = 1;
        }
    }

    void CheckDoublePress()
    {

    }

    void Control()
    {
        if (!GameObject.Find("UI").GetComponent<SceneManage>().isPaused)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (PerKeyPressTime < KeyPressInterval) {
                    PerKeyPressTime = 0;
                }

                if (KeyPressTime == 0)
                {
                    KeyPressTime = Time.time;
                    isWalking = true;
                }
                
                else if (Time.time - KeyPressTime < RunningInterval)
                {
                    isRunning = true;
                    KeyPressTime = 0;
                }
                else
                {
                    isWalking = true;
                    KeyPressTime = 0;
                }


            }

            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                isWalking = false;
                PerKeyPressTime = Time.time - KeyPressTime;
                if (isRunning) 
                { 
                    isRunning = false;
                    KeyPressTime = 0;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                isJumpStart = true;
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumpStart = false;
                isJump = true;
                isGround = false;
            }

            if (Input.GetKey(KeyCode.X))
            {
                isAttack = true;
            }
            else
            {
                isAttack = false;
            }
        }

    }

    void WeaponControl()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !GetComponent<GrapplingHook>().isAttach) {
            weaponPos++;
            if (weaponPos > 3)
                weaponPos = 0;
        }
        switch (weaponPos)
        {
            case 0:
                weapon = Weapons.GUNS; break;
            case 1:
                weapon = Weapons.KNIFE; break;
            case 2:
                weapon = Weapons.ROPE; break;
            default: break;

        }
    }

    void FrictionUpdate()
    {

    }
    void Walk()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed;

        if (grappling.isAttach)
        {
            LineRenderer Line = GetComponent<GrapplingHook>().line;
            Vector2 lineVec = (transform.position - Line.GetPosition(1));
            if (lineVec.x < 0)
                direction = Direction.LEFT;
            else
                direction = Direction.RIGHT;

            rigid.AddForce(new Vector2(x, rigid.velocity.y));
        }
        else
        {
            if (x > 0)
                direction = Direction.RIGHT;
            else if (x < 0)
                direction = Direction.LEFT;
            rigid.velocity = new Vector2(x, rigid.velocity.y);
        }
            
    }

    void Run()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * 2;

        if (!grappling.isAttach)
        {
            if (x > 0)
                direction = Direction.RIGHT;
            else if (x < 0)
                direction = Direction.LEFT;
            rigid.velocity = new Vector2(x, rigid.velocity.y);
        }
    }

    void Attack()
    {
        if (weapon == Weapons.GUNS && isGround) { isShooting = true; }
        else { isShooting = false; }
        if (weapon == Weapons.KNIFE && isGround) { isFencing = true; }
        else { isFencing = false; }
        if (weapon == Weapons.ROPE && !isGround) 
        {
            isGrapplingShot = true; 
        }
        else { isGrapplingShot = false; }
    }

    void Jump()
    { 
        rigid.AddForce(Vector3.up * jumpV, ForceMode2D.Impulse);
        if (isJump)
        {
            jumpCount = 0;
        }
    }

    void Update()
    {
        Control();
        WeaponControl();
        if (isAttack)
        {
            Attack();
        }
        else
        {
            isShooting = false;
            isFencing = false;
            isGrapplingShot= false;
        }
    }

    void FixedUpdate()
    {
        if (grappling.isAttach)
        {
            LineRenderer Line = GetComponent<GrapplingHook>().line;
            Vector2 lineVec = (Line.GetPosition(1) - transform.position);
            Debug.DrawLine(transform.position, Line.GetPosition(1));
            Debug.DrawLine(Line.GetPosition(0), Line.GetPosition(1));
            if (lineVec.x < 0)
                direction = Direction.LEFT;
            else
                direction = Direction.RIGHT;
        }
        if (isWalking)
        {
            Walk();
        }
        if (isRunning)
        {
            Run();
        }
        if (isJump && jumpCount == 1)
        {
            Jump();
        }
        if (rigid.velocity.y < 0)
        {
            isLanding = true;
        }
        else
        {
            isLanding = false;
        }
        //Debug.Log(KeyPressTime);
    }
}
