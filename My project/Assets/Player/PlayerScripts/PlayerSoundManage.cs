using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeroundManage : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Knife;

    public AudioSource Walk;
    public AudioSource Run;
    public AudioSource Jump;
    public AudioSource Land;

    public AudioSource PistolShot;
    public AudioSource PistolReload;
    public AudioSource RifleShot;

    PlayerControl playerControl;
    StatManage Pstm;
    GunManage PistolM;
    GunManage RifleM;
    KnifeManage KnifeM;
    Animator playerAnim;
    AnimatorStateInfo curAnim;

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
        playerControl = GetComponent<PlayerControl>();
        PistolM = Pistol.GetComponent<GunManage>();
        RifleM = Rifle.GetComponent<GunManage>();
        KnifeM = Knife.GetComponent<KnifeManage>();
        playerAnim = GetComponent<Animator>();
        Pstm = GetComponent<StatManage>();
    }

    void MoveSoundUpdate()
    {
        isWalk = playerControl.isWalking;
        isRun = playerControl.isRunning;
        isJumpStart = playerControl.isJumpStart;

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

    }

    void WeaponSoundUpdate()
    {
        isShot = PistolM.isShot;
        isReload = PistolM.isReload;
        if (isShot)
        {
            PistolShot.Play();
        }
        if (isReload && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            PistolReload.Play();
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
