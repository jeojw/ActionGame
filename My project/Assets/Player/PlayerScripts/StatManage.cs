
using UnityEngine;

public class StatManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HeadHitbox;
    public GameObject BodyHitbox;
    public GameObject RightLegHitbox_1;
    public GameObject RightLegHitbox_2;
    public GameObject LeftLegHitbox_1;
    public GameObject LeftLegHitbox_2;

    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Knife;

    PistolManage PistolM;
    RifleManage RifleM;

    PlayerCheckCollision HeadCheck;
    PlayerCheckCollision BodyCheck;
    PlayerCheckCollision RightLegCheck_1;
    PlayerCheckCollision RightLegCheck_2;
    PlayerCheckCollision LeftLegCheck_1;
    PlayerCheckCollision LeftLegCheck_2;

    private bool UseItem = false;
    ItemManage.ITEMTYPE ItemType;

    PlayerControl playControl;

    private float Damage = 0;
    private bool _GetHit = false;
    private bool _isDead;
    public bool GetHit { 
        get { return _GetHit; }
        set { _GetHit = value; }
    }

    public bool isDead { 
        get { return _isDead; }
        set { _isDead = value; }
    }

    public float MaxHp;
    public float curHp;

    public float GunDamage;
    public float GunShotDelay;

    void Start()
    {
        HeadCheck = HeadHitbox.GetComponent<PlayerCheckCollision>();
        BodyCheck = BodyHitbox.GetComponent<PlayerCheckCollision>();
        RightLegCheck_1 = RightLegHitbox_1.GetComponent<PlayerCheckCollision>();
        RightLegCheck_2 = RightLegHitbox_2.GetComponent<PlayerCheckCollision>();
        LeftLegCheck_1 = LeftLegHitbox_1.GetComponent<PlayerCheckCollision>();
        LeftLegCheck_2 = LeftLegHitbox_2.GetComponent<PlayerCheckCollision>();

        PistolM = Pistol.GetComponent<PistolManage>();
        RifleM = Rifle.GetComponent<RifleManage>();

        playControl = GetComponent<PlayerControl>();
        curHp = MaxHp;
        ItemType = ItemManage.ITEMTYPE.NONE;

        _isDead = false;
    }

    void ATKUpdate()
    {
        if (ItemType == ItemManage.ITEMTYPE.RIFLE ||
            ItemType == ItemManage.ITEMTYPE.PISTOL)
        {
            UseItem = false;
            if (ItemType == ItemManage.ITEMTYPE.RIFLE)
            {
                UseItem = true;
                RifleM.GetMagazine();
                if (RifleM.curMagazines > 0 || !RifleM.AmmunitionZero)
                {
                    GunDamage = RifleM.BulletDamage;
                    GunShotDelay = RifleM.ShotDelay;
                    playControl.SetShotDelay(GunShotDelay);
                }
                ItemType = ItemManage.ITEMTYPE.NONE;
            }
            if (ItemType == ItemManage.ITEMTYPE.PISTOL)
            {
                UseItem = true;
                PistolM.GetMagazine();
                ItemType = ItemManage.ITEMTYPE.NONE;
            }
        }

        else
        {
            UseItem = false;
        }

        if (RifleM.AmmunitionZero && RifleM.curMagazines == 0)
        {
            GunDamage = PistolM.BulletDamage;
            GunShotDelay = PistolM.ShotDelay;
            playControl.SetShotDelay(GunShotDelay);
        }
        else
        {
            GunDamage = RifleM.BulletDamage;
            GunShotDelay = RifleM.ShotDelay;
            playControl.SetShotDelay(GunShotDelay);
        }
    }

    public void ResetHp()
    {
        curHp = MaxHp;
    }

    public void SetGetItem(ItemManage.ITEMTYPE _itemType)
    {
        ItemType = _itemType;
    }

    public void SetHp(float _hp)
    {
        MaxHp = _hp;
    }
    public void SetGetDamage(float _damage)
    {
        Damage = _damage;
    }

    void ResetDamage()
    {
        Damage = 0;
    }
    void HpUpdate()
    {
        if (curHp > 0)
        {
            _isDead = false;
            if (Damage != 0)
            {
                curHp -= Damage;
                ResetDamage();
            }

            if (playControl.isGetItem)
            {
                
                UseItem = false;
                if (ItemType == ItemManage.ITEMTYPE.HEAL && !UseItem)
                {
                    curHp += 50f;
                    UseItem = true;
                }
            }
            if (playControl.isOnDead)
                curHp = 0;
        }
        else
            _isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        _GetHit = (HeadCheck.isHit || BodyCheck.isHit ||
                 RightLegCheck_1.isHit || RightLegCheck_2.isHit ||
                 LeftLegCheck_1.isHit || LeftLegCheck_2.isHit);

        HpUpdate();
        ATKUpdate();
    }
}
