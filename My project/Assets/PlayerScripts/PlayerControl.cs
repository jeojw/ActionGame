using System;
using System.Collections;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    public Transform Foot;
    GrapplingHook grappling;
    private Animator anim;
    public Transform bodyPos;
    public GameObject WeaponUI;
    public AudioSource Walking;
    public AudioSource Running;
    public AudioSource Jumping;
    public AudioSource Landing;

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public enum Weapons
    {
        NONE,
        GUNS,
        KNIFE,
        ROPE
    }

    enum SPEEDCOEF
    {
        ROPEWALK = 1 / 2,
        WALK = 1,
        RUN = 2
    }

    public bool isSlope = false;

    private int jumpCount = 1;
    public float speed;
    public float jumpV;
    private int weaponPos = 0;

    public float RopeDelay;
    private float RopeDelayStart = 0;
    public float RopeDelayElapsed = 0;
    public float ShotDelay;
    private float ShotDelayStart = 0;
    public float ShotDelayElapsed = 0;

    private float KeyPressTime = 0;
    private float PerKeyPressTime = 0;
    private float KeyPressInterval = 0.5f;
    public float RunningInterval = 0.5f;
    public bool pressedFirstTime = false;
    private float lastPressedTime;

    public Direction direction = Direction.RIGHT;
    public Weapons weapon;

    public bool AimingGun = false;
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

    private float SlopeAngle;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        grappling = GetComponent<GrapplingHook>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void CheckOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bodyPos.position, new Vector2(2, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        RaycastHit2D hit2 = Physics2D.BoxCast(bodyPos.position, new Vector2(2, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("Slope"));

        if (hit.collider != null || hit2.collider != null)
        {
            isGround = true;
            jumpCount = 1;
        }
        else
        {
            isGround = false;
        }

    }

    private void checkSlope()
    {
        float x = Input.GetAxis("Horizontal");
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));
        RaycastHit2D hit2 = Physics2D.BoxCast(bodyPos.position, new Vector2(2, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("Slope"));
        if (hit.collider != null)
        {
            if (hit.collider.name == "Slope")
                isSlope = true;
        }
        else
            isSlope = false;

        if (hit2.collider != null)
        {
            if (hit2.collider.name == "Slope")
                isSlope = true;
        }
        else
            isSlope = false;
        
    }

    void Control()
    {
        if (!GameObject.Find("UI").GetComponent<SceneManage>().isPaused)
        {
            if (!isRolling)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    if (weapon == Weapons.NONE)
                    {
                        if (PerKeyPressTime < KeyPressInterval)
                        {
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
                    else
                    {
                        isWalking = true;
                    }
                }

                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    isWalking = false;
                    if (weapon == Weapons.NONE)
                    {
                        PerKeyPressTime = Time.time - KeyPressTime;
                        if (isRunning)
                        {
                            isRunning = false;
                            KeyPressTime = 0;
                        }
                    }


                }
            }
            
            if (!isRolling)
            {
                if (Input.GetKeyDown(KeyCode.Space) && 
                    !WeaponUI.GetComponent<WeaponUIManage>().IsZeroPistol && 
                    (weapon == Weapons.NONE ||
                    weapon == Weapons.ROPE) &&
                    jumpCount == 1)
                {
                    isJumpStart = true;
                }

                else if (Input.GetKeyUp(KeyCode.Space) && !WeaponUI.GetComponent<WeaponUIManage>().IsZeroPistol &&
                    (weapon == Weapons.NONE ||
                    weapon == Weapons.ROPE))
                {
                    isJumpStart = false;
                    isJump = true;
                    jumpCount = 0;
                }
            }
            

            if (isWalking && !isRunning && !isJump && Input.GetKey(KeyCode.LeftShift))
            {
                isRolling = true;
            }
            else
                isRolling = false;

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (weapon == Weapons.GUNS && ShotDelayElapsed == 0)
                {
                    isAttack = true;
                    ShotDelayStart = Time.time;
                }
                if (weapon == Weapons.ROPE && RopeDelayElapsed == 0)
                {
                    isAttack = true;
                    RopeDelayStart = Time.time;
                }

            }
            else
            {
                isAttack = false;
                AttackCooldown();
            }
        }

    }

    void AttackCooldown()
    {
        
        float delay;
        if (weapon == Weapons.GUNS)
        {
            ShotDelayElapsed = Time.time - ShotDelayStart;
            delay = ShotDelay;
            if (ShotDelayElapsed >= delay)
            {
                ShotDelayElapsed = 0;
                ShotDelayStart = 0;
            }
        }
        else if (weapon == Weapons.ROPE)
        {
            RopeDelayElapsed = Time.time - RopeDelayStart;
            if (RopeDelayElapsed >= RopeDelay)
            {
                RopeDelayElapsed = 0;
                RopeDelayStart = 0;
            }
        }
    }

    void WeaponControl()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && 
            !GetComponent<GrapplingHook>().isAttach &&
            !GetComponent<GrapplingHook>().isHookActive &&
            !GetComponent<GrapplingHook>().isLineMax &&
            isGround) {
            weaponPos++;
            if (weaponPos > 4)
                weaponPos = 0;
        }
        switch (weaponPos)
        {
            case 0:
                weapon = Weapons.NONE; break;
            case 1:
                weapon = Weapons.GUNS; break;
            case 2:
                weapon = Weapons.KNIFE; break;
            case 3:
                weapon = Weapons.ROPE; break;
            default: break;

        }
    }

    void FrictionUpdate()
    {

    }
    void Walk()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (grappling.isAttach || grappling.isHookActive)
        {
            rigid.AddForce(new Vector2(x * speed, rigid.velocity.y));
        }

        else
        {
            if (x > 0) 
                direction = Direction.RIGHT;
            else if (x < 0)
                direction = Direction.LEFT;

            if (isSlope)
            {
                RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));
                SlopeAngle = Vector2.Angle(Vector2.up, hit.normal);

                rigid.velocity = Vector3.ProjectOnPlane(Vector3.right * x, hit.normal).normalized * speed + Vector3.down * rigid.gravityScale;

            }
            else
                rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
        }
    }

    void Run()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (!grappling.isAttach)
        {
            if (x > 0)
                direction = Direction.RIGHT;
            else if (x < 0)
                direction = Direction.LEFT;
            if (isSlope)
            {
                RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));
                SlopeAngle = Vector2.Angle(Vector2.up, hit.normal);

                rigid.velocity = Vector3.ProjectOnPlane(Vector3.right * x, hit.normal).normalized * speed * 2 + Vector3.down * rigid.gravityScale * 2;

            }
            else
                rigid.velocity = new Vector2(x * speed * 2, rigid.velocity.y);
        }
    }

    void Attack()
    {
        if (weapon == Weapons.GUNS && isGround && !WeaponUI.GetComponent<WeaponUIManage>().IsZeroPistol) 
        {
            isShooting = true; 
        }
        else 
        {
            isShooting = false; 
        }
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
            isJump = false;
            jumpCount = 0;
        }
    }

    void Rolling()
    {
        float x = Input.GetAxis("Horizontal");

        if (isSlope)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));
            SlopeAngle = Vector2.Angle(Vector2.up, hit.normal);

            rigid.velocity = Vector3.ProjectOnPlane(Vector3.right * x, hit.normal).normalized * speed * 2f + Vector3.down * rigid.gravityScale;

        }
        else
            rigid.velocity = new Vector2(x * speed * 2f, rigid.velocity.y);
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
        checkSlope();
        CheckOnGround();

        if (isWalking)
        {
            Walk();
            //Walking.Play();
        }
        else
            Walking.Stop();
        if (isRunning)
        {
            Run();
            //Running.Play();
        }
        else
            Running.Stop();
        if (isJump)
        {
            Jump();
            //Jumping.Play();
        }
        else
            Jumping.Stop();
        if (isRolling || 
            (anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling") && 
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
        {
            Rolling();
        }
        if (rigid.velocity.y < 0)
        {
            isLanding = true;
            //Landing.Play();
        }
        else
        {
            isLanding = false;
            //Landing.Stop(); 
        }
    }
}
