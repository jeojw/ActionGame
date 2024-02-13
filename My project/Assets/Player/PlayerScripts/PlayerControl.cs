using System;
using System.Collections;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    public Transform Foot;
    GrapplingHook grappling;
    private Animator anim;
    public Transform bodyPos;
    public GameObject UI;
    public GameObject Pistol;
    
    SceneManage sceneManage;
    WeaponUIManage weaponUIManage;
    StatManage statManage;
    GunManage gunManage;
    public enum Direction
    {
        LEFT = -1,
        RIGHT = 1
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

    private float lastKeyPressTime = 0;
    public float RunningInterval = 0.5f;
    public bool pressedFirstTime = false;

    public float KnifeAttackInterval;
    private float KnifeAttackStart = 0;
    private float KnifeAttackElapsed = 0;
    public int KnifeStep = 1;

    public Direction direction = Direction.RIGHT;
    public Weapons weapon;

    public bool AimingGun = false;
    public bool isShooting = false; 
    public bool isReloading = false;
    public bool isFencing = false;
    public bool isGrapplingShot = false;
    public bool isPunching = false;

    public bool isMoving = false;
    public bool isJumpStart = false;
    public bool isJump = false;
    public bool isLanding = false;
    public bool isRolling = false;
    public bool isAttack = false;
    public bool isWalking = false;
    public bool isGround = false;
    public bool isRunning = false;
    public bool isLowerBody = false;

    public bool IsDoublePressed = false;

    private float SlopeAngle;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        grappling = GetComponent<GrapplingHook>();
        anim = GetComponent<Animator>();
        sceneManage = UI.GetComponent<SceneManage>();
        weaponUIManage = UI.GetComponent<WeaponUIManage>();
        statManage = GetComponent<StatManage>();
        gunManage = Pistol.GetComponent<GunManage>();
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
            jumpCount = 0;
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
        if (!sceneManage.isPaused || !statManage.isDead)
        {
            if (isWalking || isRunning || isJump || isLanding || isJumpStart)
                isMoving = true;
            else
                isMoving = false;

            if (!isRolling)
            {
                bool leftPressed = Input.GetKeyDown(KeyCode.A);
                bool rightPressed = Input.GetKeyDown(KeyCode.D);

                bool leftPressing = Input.GetKey(KeyCode.A);
                bool rightPressing = Input.GetKey(KeyCode.D);
                if (leftPressed)
                {
                    IsDoublePressed = (direction == Direction.LEFT && Time.time - lastKeyPressTime < RunningInterval);
                    direction = Direction.LEFT;
                    lastKeyPressTime = Time.time;
                }
                if (rightPressed)
                {
                    IsDoublePressed = (direction == Direction.RIGHT && Time.time - lastKeyPressTime < RunningInterval);
                    direction = Direction.RIGHT;
                    lastKeyPressTime = Time.time;
                }

                if (leftPressing || rightPressing)
                {
                    if (IsDoublePressed && weapon == Weapons.NONE)
                    {
                        isWalking = false;
                        isRunning = true;
                    }
                    else
                    {
                        isWalking = true;
                        isRunning = false;
                    }
                }
                else
                {
                    isWalking = false;
                    isRunning = false;
                }
            }


            if (!isRolling)
            {
                if (Input.GetKeyDown(KeyCode.Space) && 
                    !weaponUIManage.IsZeroPistol && 
                    (weapon == Weapons.NONE ||
                    weapon == Weapons.ROPE) &&
                    jumpCount == 1)
                {
                    isJumpStart = true;
                }

                else if (Input.GetKeyUp(KeyCode.Space) && !weaponUIManage.IsZeroPistol &&
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

            if (Input.GetKey(KeyCode.S))
                isLowerBody = true;
            else
                isLowerBody = false;

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (weapon == Weapons.NONE)
                    isAttack = true;
                
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
                if (weapon == Weapons.KNIFE)
                {
                    isAttack = true;
                    KnifeAttackStart = Time.time;
                }
            }
            else
            {
                isAttack = false;
                AttackCooldown();
            }
            if (weapon == Weapons.KNIFE)
                KnifeFightControl();
        }

    }

    void KnifeFightControl()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_1"))
        {
            if (Input.GetKeyDown(KeyCode.X))
                KnifeStep = 2;
            else
            {
                isAttack = false;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_2"))
        {
            if (Input.GetKeyDown(KeyCode.X))
                KnifeStep = 3;
            else
            {
                isAttack = false;
                if (!anim.GetBool("isFencing_2") && !anim.GetBool("isFencing_3"))
                    KnifeStep = 1;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knife_Fighting_3"))
        {

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isAttack = false;
                KnifeStep = 1;
            }
        }
    }

    void AttackCooldown()
    {
        if (weapon == Weapons.GUNS)
        {
            ShotDelayElapsed = Time.time - ShotDelayStart;
            if (ShotDelayElapsed >= ShotDelay)
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
        else if (weapon == Weapons.KNIFE)
        {
            KnifeAttackElapsed = Time.time - KnifeAttackStart;
            if (KnifeAttackElapsed >= KnifeAttackInterval)
            {
                KnifeAttackElapsed = 0;
                KnifeAttackStart = 0;
            }
        }
    }

    void WeaponControl()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && 
            !grappling.isAttach &&
            !grappling.isHookActive &&
            !grappling.isLineMax &&
            !isLowerBody &&
            !isMoving &&
            !gunManage.isReload &&
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

        if (grappling.isAttach || grappling.isHookActive)
        {
            rigid.AddForce(new Vector2((int)direction * speed, rigid.velocity.y));
        }

        else
        {
            if (isSlope)
            {
                RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));
                SlopeAngle = Vector2.Angle(Vector2.up, hit.normal);

                rigid.velocity = Vector3.ProjectOnPlane(Vector3.right * (int)direction, hit.normal).normalized * speed + Vector3.down * rigid.gravityScale;

            }
            else
                rigid.velocity = new Vector2((int)direction * speed, rigid.velocity.y);
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
        if (weapon == Weapons.NONE)
            isPunching = true;
        else
            isPunching = false;
        if (weapon == Weapons.GUNS && isGround && !weaponUIManage.IsZeroPistol) 
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

    void GetAttack()
    {

    }

    void Update()
    {
        if (!statManage.isDead)
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
                isGrapplingShot = false;
                isPunching = false;
            }
        }
        
    }

    void FixedUpdate()
    {
        checkSlope();
        CheckOnGround();
        if (!statManage.isDead)
        {
            if (isWalking)
            {
                Walk();
            }
            if (isRunning)
            {
                Run();
            }
            if (isJump)
            {
                Jump();
            }
            if (isRolling ||
                (anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
            {
                Rolling();
            }
            if (rigid.velocity.y < 0)
            {
                isLanding = true;
            }
            else
            {
                isLanding = false;
            }
        }
        
    }
}
