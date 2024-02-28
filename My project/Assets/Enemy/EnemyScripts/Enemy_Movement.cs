using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Pos;
    Animator Enemyanim;
    public enum ATTACKTYPE
    {
        PISTOL,
        SWORD,
        RIFLE
    }

    public enum DIRECTION
    {
        LEFT = -1,
        RIGHT = 1
    }

    public ATTACKTYPE AttackType;
    public DIRECTION detectDirection;

    public GameObject Pistol;
    public GameObject Rifle;
    Enemy_GunManage PistolM;
    Enemy_GunManage RifleM;

    Rigidbody2D rigid;
    Enemy_StatManage Estat;

    public bool isDetect = false;
    public bool isWalking = false;
    public bool isAttack = false;
    public bool isFencing = false;
    public bool isShooting = false;
    public bool isDead = false;
    public bool GetCool = false;

    private bool isZero;

    private float AttackCoolTime;
    private float AttackCoolStart = 0;
    private float _AttackcoolElapsed = 0;
    public float AttackCoolElapsed
    {
        get { return _AttackcoolElapsed; }
        set
        {
            _AttackcoolElapsed = value;
        }
    }
    private int ShotCount = 0; 

    private bool isHit;

    private float NDetectInterval =40f;
    private float RifleDetectInterval = 60f;
    public float speed;

    private float AtkRange;
    private float KnifeRange = 10f;
    private float PistolRange = 50f;
    private float RifleRange = 55f;

    void Start()
    {
        Enemyanim = GetComponent<Animator>();
        Estat = GetComponent<Enemy_StatManage>();
        PistolM = Pistol.GetComponent<Enemy_GunManage>();
        RifleM = Rifle.GetComponent<Enemy_GunManage>();
        detectDirection = DIRECTION.LEFT; 
        rigid = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3((float)detectDirection, 1, 1);
    }

    void SetAtkType()
    {
        if (AttackType == ATTACKTYPE.SWORD)
        {
            AtkRange = KnifeRange;
            AttackCoolTime = 3f;
        }
        else if (AttackType == ATTACKTYPE.PISTOL)
        {
            AtkRange = PistolRange;
            AttackCoolTime = PistolM.ShotDelay;
            isZero = PistolM.isZero;
        }
        else if (AttackType == ATTACKTYPE.RIFLE)
        {
            AtkRange = RifleRange;
            AttackCoolTime = RifleM.ShotDelay;
            isZero = RifleM.isZero;
        }
    }

    public void SetAttackType(ATTACKTYPE _attackType)
    {
        AttackType = _attackType;
    }

    void DetectivePlayer()
    {
        RaycastHit2D hit1;
        RaycastHit2D hit2;
        if (AttackType != ATTACKTYPE.RIFLE)
        {
            hit1 = Physics2D.Raycast(Pos.position, Vector2.left, NDetectInterval, LayerMask.GetMask("Player"));
            hit2 = Physics2D.Raycast(Pos.position, Vector2.right, NDetectInterval, LayerMask.GetMask("Player"));
        }
        else
        {
            hit1 = Physics2D.Raycast(Pos.position, Vector2.left, RifleDetectInterval, LayerMask.GetMask("Player"));
            hit2 = Physics2D.Raycast(Pos.position, Vector2.right, RifleDetectInterval, LayerMask.GetMask("Player"));
        }
        
        if (hit1.collider != null || hit2.collider != null)
        {
            isDetect = true;
            if (hit1.collider != null)
                detectDirection = DIRECTION.LEFT;
            else if (hit2.collider != null)
                detectDirection = DIRECTION.RIGHT;
        }
        else
            isDetect = false;
    }
    void Enemy_AI()
    {
        RaycastHit2D AttackHit = Physics2D.BoxCast(Pos.position, new Vector2(AtkRange, 8), 0, (int)detectDirection * Vector2.left, AtkRange, LayerMask.GetMask("Player"));
        if (isDetect)
        {
            if (!AttackHit)
                isWalking = true;

            if (AttackHit) 
            {
                isWalking = false;
                if (!GetCool)
                {
                    if (AttackType == ATTACKTYPE.SWORD)
                    {
                        isAttack = true;
                        AttackCoolStart = Time.time;
                    }
                    else
                    {
                        if (!isZero)
                        {
                            isAttack = true;
                            AttackCoolStart = Time.time;
                        }
                    }
                }
            }
            if (GetCool || isZero)
            {
                isAttack = false;
                AttackCooldown();
            }
        }
        else
        {
            isWalking = false;
            isAttack = false;
        }

    }

    void Move()
    {
        if (detectDirection == DIRECTION.LEFT)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rigid.velocity = new Vector2(speed * (-1), rigid.velocity.y);
        }

        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        }
    }

    void Attack()
    {
        if (AttackType == ATTACKTYPE.SWORD)
        {
            isFencing = true;
            if (Enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Fencing_Enemy") &&
                Enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                GetCool = true;
            }
        }
        else if (AttackType == ATTACKTYPE.PISTOL ||
                 AttackType == ATTACKTYPE.RIFLE)
        {
            isShooting = true;
            if (AttackType == ATTACKTYPE.PISTOL)
                PistolM.Shot();
            else if (AttackType == ATTACKTYPE.RIFLE)
                RifleM.Shot();
            if (ShotCount == 0)
                ShotCount = 1;
            if (ShotCount == 1)
            {
                GetCool = true;
                ShotCount = 0;
            }
        }
    }

    void AttackCooldown()
    {
        _AttackcoolElapsed = Time.time - AttackCoolStart;
        if (_AttackcoolElapsed >= AttackCoolTime)
        {
            _AttackcoolElapsed = 0;
            AttackCoolStart = 0;
            GetCool = false;
        }
    }

    void FixedUpdate()
    {
        if (isWalking && !isHit)
            Move();

        if (isAttack)
            Attack();
        else
        {
            isFencing = false;
            isShooting = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetAtkType();
        isDead = Estat.isDead;
        DetectivePlayer();
        if (!isDead)
            Enemy_AI();
        else
        {
            isFencing = false;
            isWalking = false;
            isAttack = false;
            isDetect = false;
            isShooting = false;
        }
    }
}
