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

    public enum CONDITION
    {
        LIVE,
        DEAD
    }

    public enum DIRECTION
    {
        LEFT = -1,
        RIGHT = 1
    }

    public ATTACKTYPE AttackType;
    public CONDITION Condition;
    public DIRECTION detectDirection;
    public GameObject Player;
    Rigidbody2D rigid;
    Enemy_StatManage Estat;

    public bool isDetect = false;
    public bool isWalking = false;
    public bool isAttack = false;
    public bool isFencing = false;
    public bool isShooting = false;
    public bool isDead = false;
    public bool GetCool = false;

    public float AttackCoolTime;
    private float AttackCoolStart = 0;
    private float AttackCoolElapsed = 0;

    public float DetectInterval;
    public float AttackInterval;
    public float speed;

    void Start()
    {
        Enemyanim = GetComponent<Animator>();
        Estat = GetComponent<Enemy_StatManage>();
        AttackType = ATTACKTYPE.SWORD;
        detectDirection = DIRECTION.LEFT; 
        rigid = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3((float)detectDirection, 1, 1);
    }

    void DetectivePlayer()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(Pos.position, Vector2.left, DetectInterval, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.Raycast(Pos.position, Vector2.right, DetectInterval, LayerMask.GetMask("Player"));
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
        if (isDetect)
        {
            if (Vector2.Distance(Player.transform.position, Pos.position) >= 6f)
                isWalking = true;

            if (Vector2.Distance(Player.transform.position, Pos.position) < 6f && !GetCool) 
            {
                isAttack = true;
                isWalking = false;
                AttackCoolStart = Time.time;
            }
            if (GetCool)
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
    }

    void AttackCooldown()
    {
        AttackCoolElapsed = Time.time - AttackCoolStart;
        if (AttackCoolElapsed >= AttackCoolTime)
        {
            AttackCoolElapsed = 0;
            AttackCoolStart = 0;
            GetCool = false;
        }
    }

    void FixedUpdate()
    {
        if (isWalking)
            Move();

        if (isAttack)
            Attack();
        else
        {
            isFencing = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        isDead = Estat.isDead;
        DetectivePlayer();
        if (!isDead)
            Enemy_AI();
        Debug.Log(AttackCoolElapsed);
    }
}
