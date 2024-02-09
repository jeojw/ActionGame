using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    // Start is called before the first frame update
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
    public DIRECTION Direction;
    public GameObject Player;
    Rigidbody2D rigid;

    public bool isDetect = false;
    public bool isWalking = false;
    public bool isAttack = false;
    public bool isFencing = false;
    public bool isShooting = false;

    public float AttackCoolTime;
    private float AttackCoolStart = 0;
    private float AttackCoolElapsed = 0;

    public float DetectInterval;
    public float AttackInterval;
    public float speed;

    void Start()
    {
        AttackType = ATTACKTYPE.SWORD;
        Direction = DIRECTION.LEFT; 
        rigid = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3((float)Direction, 1, 1);
    }

    void DetectivePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * (float)Direction, DetectInterval, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            isDetect = true;
        }
        else
            isDetect = false;

        Debug.DrawRay(transform.position, Vector2.right * (float)Direction * DetectInterval, Color.blue);
    }

    void Enemy_AI()
    {
        if (isDetect)
        {
            if (Vector2.Distance(Player.transform.position, transform.position) >= 6f)
                isWalking = true;
            else
                isWalking = false;

            if (Vector2.Distance(Player.transform.position, transform.position) < 6f && AttackCoolElapsed == 0)
            {
                isAttack = true;
                AttackCoolStart = Time.time;
            }
            else
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
        if (Direction == DIRECTION.LEFT)
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
        }
    }

    void AttackCooldown()
    {
        AttackCoolElapsed = Time.time - AttackCoolStart;
        if (AttackCoolElapsed >= AttackCoolTime)
        {
            AttackCoolElapsed = 0;
            AttackCoolStart = 0;
        }
    }

    void FixedUpdate()
    {
        if (isWalking)
            Move();

        if (isAttack)
            Attack();
    }
    // Update is called once per frame
    void Update()
    {
        DetectivePlayer();
        Enemy_AI();

        Debug.Log(AttackCoolElapsed);
    }
}
