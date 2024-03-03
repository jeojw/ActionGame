using System.Runtime.CompilerServices;
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
    public GameObject UI;
    public GameObject Pistol;
    public GameObject Rifle;

    public GameObject Event;

    Vector3 PlayerPos = new Vector3(-10.82f, -0.4f, -0.1751058f);

    MainSceneManage sceneManage;
    WeaponUIManage weaponUIManage;
    StatManage statManage;
    PistolManage PistolManage;
    RifleManage RifleManage;
    public enum Direction
    {
        LEFT = -1,
        RIGHT = 1
    }

    public enum Weapons
    {
        NONE,
        RIFLE,
        PISTOL,
        KNIFE,
        ROPE
    }

    enum SPEEDCOEF
    {
        ROPEWALK = 1 / 2,
        WALK = 1,
        RUN = 2
    }

    private bool isSlope = false;

    private int jumpCount = 1;
    public float speed = 10f;
    private float jumpV = 50f;
    private int weaponPos = 0;

    private float ShotDelay;
    private float ShotDelayStart = 0;
    private float _ShotDelayElapsed = 0;
    public float ShotDelayElapsed
    {
        get { return _ShotDelayElapsed; }
        set { _ShotDelayElapsed = value; }
    }

    private float FistDelay = 0.5f;
    private float FistDelayStart = 0;
    private float FistDelayElapsed = 0;

    private float lastKeyPressTime = 0;
    public float RunningInterval = 0.5f;
    public bool pressedFirstTime = false;

    public int KnifeStep = 1;

    public Direction direction = Direction.RIGHT;
    public Weapons weapon;

    private bool _isShooting = false;
    private bool _isFencing = false;
    private bool _isPunching = false;

    public bool isShooting
    {
        get { return _isShooting; }
        set { _isShooting = value; }
    }
    public bool isFencing { 
        get { return _isFencing; } 
        set { _isFencing = value; }
    }
    public bool isPunching { 
        get { return _isPunching; } 
        set { _isPunching = value;}
    }

    private bool _isMoving = false;
    private bool _isJumpStart = false;
    private bool _isJump = false;
    private bool _isLanding = false;
    private bool _isLand = false;
    private bool _isRolling = false;
    private bool _isAttack = false;
    private bool _isWalking = false;
    private bool _isGround = false;
    private bool _isOnDead = false;
    private bool _isRunning = false;
    private bool _isLowerBody = false;
    private bool _isGetItem = false;

    public bool isMoving { 
        get { return _isMoving; }
        set { _isMoving = value; }
    }
    public bool isJumpStart { 
        get { return _isJumpStart; }   
        set { _isJumpStart = value; }
    }
    public bool isJump
    {
        get { return _isJump; }
        set { _isJump = value; }
    }
    public bool isLanding
    {
        get { return _isLanding; }
        set { _isLanding = value; }
    }
    public bool isLand
    {
        get { return _isLand; }
        set { _isLand = value; }
    }
    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }
    public bool isAttack
    {
        get { return _isAttack; }
        set { _isAttack = value; }
    }
    public bool isWalking { 
        get { return _isWalking; }
        set { _isWalking = value; }
    }
    public bool isGround { 
        get { return _isGround; }
        set { _isGround = value; }
    }
    public bool isOnDead { 
        get { return _isOnDead; }
        set { _isOnDead = value; }  
    }
    public bool isRunning { 
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public bool isLowerBody {
        get { return _isLowerBody; }
        set { _isLowerBody = value; }
    }
    public bool isGetItem { 
        get { return _isGetItem; }
        set { _isGetItem = value; }
    }

    private bool IsDoublePressed = false;

    public ItemManage.ITEMTYPE GetItemType;

    void Start()
    {
        transform.position = PlayerPos;
        rigid = GetComponent<Rigidbody2D>();
        grappling = GetComponent<GrapplingHook>();
        anim = GetComponent<Animator>();
        sceneManage = UI.GetComponent<MainSceneManage>();
        weaponUIManage = UI.GetComponent<WeaponUIManage>();
        statManage = GetComponent<StatManage>();

        PistolManage = Pistol.GetComponent<PistolManage>();
        RifleManage = Rifle.GetComponent<RifleManage>();
    }

    // Update is called once per frame

    private void CheckOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bodyPos.position, new Vector2(3, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        RaycastHit2D hit2 = Physics2D.BoxCast(bodyPos.position, new Vector2(3, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("Slope"));

        if (hit.collider != null || hit2.collider != null)
        {
            _isGround = true;
            jumpCount = 1;
        }
        else
        {
            _isGround = false;
            jumpCount = 0;
        }

    }

    private void CheckOnDeadGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bodyPos.position, new Vector2(3, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("DeadGround"));
        if (!statManage.isDead)
        {
            _isOnDead = hit.collider != null;
        }
        else
            _isOnDead = false;
    }

    private void checkSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));
        RaycastHit2D hit2 = Physics2D.BoxCast(bodyPos.position, new Vector2(4, 1), 0f, Vector2.down, 1f, LayerMask.GetMask("Slope"));


        isSlope = hit.collider != null || hit2.collider != null;
    }

    public void SetPlayerPos(Vector3 _Pos)
    {
        PlayerPos = _Pos;
    }
    public bool CheckGetItem()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bodyPos.position, new Vector2(2, 1), 0f, Vector2.down, 0.02f, LayerMask.GetMask("Item"));

        return (hit.collider != null);
    }
    void Control()
    {
        if (!sceneManage.isPaused || !statManage.isDead)
        {

            if (_isWalking || _isRunning || _isJump || _isLanding || _isJumpStart)
                isMoving = true;
            else
                isMoving = false;

            if (!_isRolling && !_isJumpStart && !_isLowerBody)
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
                        _isWalking = false;
                        _isRunning = true;
                    }
                    else
                    {
                        _isWalking = true;
                        _isRunning = false;
                    }
                }
                else
                {
                    _isWalking = false;
                    _isRunning = false;
                }
            }


            if (!isRolling && !isLowerBody)
            {
                if (Input.GetKey(KeyCode.Space) &&
                    (weapon == Weapons.NONE ||
                    weapon == Weapons.ROPE) &&
                    !_isWalking &&
                    jumpCount == 1)
                {
                    _isJumpStart = true;
                }

                else if (Input.GetKeyUp(KeyCode.Space) &&
                    (weapon == Weapons.NONE ||
                    weapon == Weapons.ROPE) &&
                    jumpCount == 1)
                {
                    _isJumpStart = false;
                    _isJump = true;
                    jumpCount = 0;
                }
            }


            if (!_isLowerBody && _isWalking && !_isRunning && !_isJump && Input.GetKey(KeyCode.LeftShift))
            {
                _isRolling = true;
            }
            else
                _isRolling = false;

            if (Input.GetKey(KeyCode.S))
                _isLowerBody = true;
            else
                _isLowerBody = false;

            if (weapon == Weapons.NONE)
                FistControl();
            if (weapon == Weapons.PISTOL || weapon == Weapons.RIFLE)
                GunControl();
            if (weapon == Weapons.KNIFE)
                KnifeFightControl();
        }

    }
    void FistControl()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (FistDelayStart == 0)
            {
                isAttack = true;
                FistDelayStart = Time.time;
            }
            else
            {
                FistDelayElapsed = Time.time - FistDelayStart;
                if (FistDelayElapsed >= FistDelay)
                {
                    FistDelayElapsed = 0;
                    FistDelayStart = 0;
                }
            }
        }
        else
        {
            FistDelayElapsed = 0;
            _isAttack = false;
        }
    }
    void GunControl()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (ShotDelayStart == 0)
            {
                _isAttack = true;
                ShotDelayStart = Time.time;
            }
            else
            {
                isAttack = false;
                _ShotDelayElapsed = Time.time - ShotDelayStart;
                if (_ShotDelayElapsed >= ShotDelay)
                {
                    _ShotDelayElapsed = 0;
                    ShotDelayStart = 0;
                }
            }
        }
        else
        {
            _ShotDelayElapsed = 0;
            _isAttack = false;
        }

    }

    void KnifeFightControl()
    {
        if (Input.GetKeyDown(KeyCode.X))
            isAttack = true;
        else
            isAttack = false;

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

    private Weapons ReturnWeapon()
    {
        switch (weaponPos)
        {
            case 0:
                weapon = Weapons.NONE; break;
            case 1:
                if (RifleManage.AmmunitionZero &&
                    RifleManage.curMagazines == 0)
                    weapon = Weapons.PISTOL;
                else
                    weapon = Weapons.RIFLE;
                break;
            case 2:
                weapon = Weapons.KNIFE; break;
            default: break;
        }

        return weapon;
    }

    public void SetShotDelay(float _Delay)
    {
        ShotDelay = _Delay;
    }
    void WeaponControl()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && 
            !grappling.isAttach &&
            !grappling.isHookActive &&
            !grappling.isLineMax &&
            !_isMoving &&
            !PistolManage.isReloading &&
            !_isJumpStart &&
            weapon != Weapons.ROPE) {
            weaponPos++;
            if (weaponPos > 2)
                weaponPos = 0;
        }
      
        if (!grappling.isAttach &&
            !grappling.isHookActive &&
            !grappling.isLineMax &&
            !_isMoving &&
            !PistolManage.isReloading &&
            !_isJumpStart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (weapon != Weapons.ROPE)
                {
                    weapon = Weapons.ROPE;
                }
                else
                {
                    weapon = ReturnWeapon();
                }
            }
        }
        if (weapon != Weapons.ROPE)
        {
            switch (weaponPos)
            {
                case 0:
                    weapon = Weapons.NONE; break;
                case 1:
                    if (RifleManage.AmmunitionZero &&
                        RifleManage.curMagazines == 0)
                        weapon = Weapons.PISTOL;

                    else
                        weapon = Weapons.RIFLE;
                    break;
                case 2:
                    weapon = Weapons.KNIFE; break;
                default: break;
            }
        }
        
    }
    void Walk()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (grappling.isAttach || grappling.isHookActive)
        {
            rigid.AddForce(new Vector2((int)direction * speed * 3, rigid.velocity.y));
        }

        else
        {
            if (isSlope)
            {
                RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));

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

                rigid.velocity = Vector3.ProjectOnPlane(Vector3.right * x, hit.normal).normalized * speed * 2 + Vector3.down * rigid.gravityScale * 2;

            }
            else
                rigid.velocity = new Vector2(x * speed * 2, rigid.velocity.y);
        }
    }

    void Attack()
    {
        if (weapon == Weapons.NONE)
            _isPunching = true;
        else
            _isPunching = false;
        if ((weapon == Weapons.RIFLE && !weaponUIManage.IsZeroRifle) || (weapon == Weapons.PISTOL && !weaponUIManage.IsZeroPistol) && _isGround) 
        {
            _isShooting = true; 
        }
        else 
        {
            _isShooting = false; 
        }
        if (weapon == Weapons.KNIFE && _isGround) 
        { 
            _isFencing = true; 
        }
        else 
        { 
            _isFencing = false; 
        }
    }

    void Jump()
    { 
        rigid.AddForce(Vector3.up * jumpV, ForceMode2D.Impulse);
        if (_isJump)
        {
            _isJump = false;
            jumpCount = 0;
        }
    }

    void Rolling()
    {
        float x = Input.GetAxis("Horizontal");

        if (isSlope)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 5f, LayerMask.GetMask("Slope"));

            rigid.velocity = Vector3.ProjectOnPlane(Vector3.right * x, hit.normal).normalized * speed * 2f + Vector3.down * rigid.gravityScale;

        }
        else
            rigid.velocity = new Vector2(x * speed * 2f, rigid.velocity.y);
    }

    public void ResetPlayer()
    {
        transform.position = PlayerPos;
        weaponPos = 0;
        PistolManage.ReloadReset();
        RifleManage.ReloadReset();
        RifleManage.ResetMagazine();
        PistolManage.ResetMagazine();
    }

    public void SetGetItem(ItemManage.ITEMTYPE _type)
    {
        GetItemType = _type;
    }

    void Update()
    {
        if (!statManage.isDead)
        {
            _isGetItem = CheckGetItem();

            Control();
            WeaponControl();
            if (_isAttack)
            {
                Attack();
            }
            else
            {
                _isShooting = false;
                _isFencing = false;
                _isPunching = false;
            }
        }
    }

    void FixedUpdate()
    {
        checkSlope();
        CheckOnGround();
        CheckOnDeadGround();
        if (!statManage.isDead)
        {
            if (_isWalking)
            {
                Walk();
            }
            if (_isRunning)
            {
                Run();
            }
            if (_isJump)
            {
                Jump();
            }
            if (_isRolling ||
                (anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
            {
                Rolling();
            }
            if (rigid.velocity.y < 0 && !isGround)
            {
                _isLanding = true;
            }
            else
            {
                _isLanding = false;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Land"))
            {
                _isLand = true;
            }
            else
                _isLand = false;
        }
    }
}
