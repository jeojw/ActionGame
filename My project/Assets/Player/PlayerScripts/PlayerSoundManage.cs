using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeroundManage : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Knife;

    public GameObject MoveSound;
    public GameObject WeaponSound;
    public GameObject ConditionSound;

    AudioSource Walk;
    AudioSource Run;
    AudioSource Jump;
    AudioSource Land;

    AudioSource PistolShot;
    AudioSource PistolReload;
    AudioSource RifleShot;
    AudioSource RifleReload;

    AudioSource Pain;
    AudioSource Dead;

    PlayerControl playerControl;
    StatManage Pstm;
    PistolManage PistolM;
    RifleManage RifleM;
    KnifeManage KnifeM;
    Animator playerAnim;

    float moveCool;
    float moveCoef = 2;

    bool isReload;
    bool isShot;
    bool isFence;

    bool isWalk;
    bool isRun;
    bool isJumpStart;
    bool isLand;
    // Start is called before the first frame update
    void Start()
    {
        Walk = MoveSound.transform.GetChild(0).GetComponent<AudioSource>();
        Run = MoveSound.transform.GetChild(1).GetComponent<AudioSource>();
        Jump = MoveSound.transform.GetChild(2).GetComponent<AudioSource>();
        Land = MoveSound.transform.GetChild(3).GetComponent<AudioSource>();

        PistolShot = WeaponSound.transform.GetChild(0).GetComponent<AudioSource>();
        PistolReload = WeaponSound.transform.GetChild(1).GetComponent<AudioSource>();
        RifleShot = WeaponSound.transform.GetChild(2).GetComponent<AudioSource>();
        RifleReload = WeaponSound.transform.GetChild(3).GetComponent<AudioSource>();

        Pain = ConditionSound.transform.GetChild(0).GetComponent<AudioSource>();
        Dead = ConditionSound.transform.GetChild(1).GetComponent<AudioSource>();

        playerControl = GetComponent<PlayerControl>();
        PistolM = Pistol.GetComponent<PistolManage>();
        RifleM = Rifle.GetComponent<RifleManage>();
        KnifeM = Knife.GetComponent<KnifeManage>();
        playerAnim = GetComponent<Animator>();
        Pstm = GetComponent<StatManage>();
    }

    void MoveSoundUpdate()
    {
        isWalk = playerControl.isWalking;
        isRun = playerControl.isRunning;
        isJumpStart = playerControl.isJumpStart;
        isLand = playerControl.isLand;

        moveCool += Time.deltaTime * moveCoef;

        if (moveCool >= 1.0f)
        {
            moveCool = 0;
            if (isWalk && playerControl.isGround)
            {
                Walk.Play();
                moveCoef = 2;
            }
                
            if (isRun)
            {
                Run.Play();
                moveCoef = 4;
            }
        }

        if (isJumpStart)
            Jump.Play();

        if (isLand)
        {
            Land.Play();
        }

        if (Pstm.GetHit && !Pstm.isDead)
            Pain.Play();

        if (Pstm.isDead)
            Dead.Play();
    }

    void WeaponSoundUpdate()
    {
        if (playerControl.weapon == PlayerControl.Weapons.PISTOL)
        {
            isShot = PistolM.isShot;
            if (isShot)
            {
                PistolShot.Play();
            }
            if ((playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol_Lower"))
                && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                PistolReload.Play();
            }
        }
        else if (playerControl.weapon == PlayerControl.Weapons.RIFLE)
        {
            isShot = RifleM.isShot;
            isReload = RifleM.isReload;
            if (isShot)
            {
                RifleShot.Play();
            }
            if ((playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Rifle") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Reloading_Rifle_Lower"))
                && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                RifleReload.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pstm.isDead)
        {
            MoveSoundUpdate();
            WeaponSoundUpdate();
        }
        
    }
}
