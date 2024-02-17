using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class StatManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HeadHitbox;
    public GameObject BodyHitbox_1;
    public GameObject BodyHitbox_2;
    public GameObject RightLegHitbox_1;
    public GameObject RightLegHitbox_2;
    public GameObject LeftLegHitbox_1;
    public GameObject LeftLegHitbox_2;

    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Knife;

    KnifeManage KnifeM;
    PistolManage PistolM;
    RifleManage RifleM;

    PlayerCheckCollision HeadCheck;
    PlayerCheckCollision BodyCheck_1;
    PlayerCheckCollision BodyCheck_2;
    PlayerCheckCollision RightLegCheck_1;
    PlayerCheckCollision RightLegCheck_2;
    PlayerCheckCollision LeftLegCheck_1;
    PlayerCheckCollision LeftLegCheck_2;

    public bool UseItem = false;
    ItemManage.ITEMTYPE ItemType;

    PlayerControl playControl;

    float ATK;
    float Damage = 0;
    bool GetHit = false;

    public bool isDead;

    public enum ATTACKCOEF
    {
        PISTOL = 10,
        KNIFE = 10,
        RIFLE = 10
    }

    public float MaxHp;
    public float curHp;

    public float GunDamage;
    public float GunShotDelay;

    void Start()
    {
        HeadCheck = HeadHitbox.GetComponent<PlayerCheckCollision>();
        BodyCheck_1 = BodyHitbox_1.GetComponent<PlayerCheckCollision>();
        BodyCheck_2 = BodyHitbox_2.GetComponent<PlayerCheckCollision>();
        RightLegCheck_1 = RightLegHitbox_1.GetComponent<PlayerCheckCollision>();
        RightLegCheck_2 = RightLegHitbox_2.GetComponent<PlayerCheckCollision>();
        LeftLegCheck_1 = LeftLegHitbox_1.GetComponent<PlayerCheckCollision>();
        LeftLegCheck_2 = LeftLegHitbox_2.GetComponent<PlayerCheckCollision>();

        PistolM = Pistol.GetComponent<PistolManage>();
        RifleM = Rifle.GetComponent<RifleManage>();
        KnifeM = Knife.GetComponent<KnifeManage>();

        playControl = GetComponent<PlayerControl>();
        curHp = MaxHp;

        isDead = false;
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
                RifleM.ResetMagazine();
                GunDamage = RifleM.BulletDamage;
                GunShotDelay = RifleM.ShotDelay;
                playControl.SetShotDelay(GunShotDelay);
                ItemType = ItemManage.ITEMTYPE.NONE;
            }
            if (ItemType == ItemManage.ITEMTYPE.PISTOL)
            {
                UseItem = true;
                PistolM.ResetMagazine();
                ItemType = ItemManage.ITEMTYPE.NONE;
            }
        }

        else
        {
            UseItem = false;
        }

        if (RifleM.AmmunitionZero)
        {
            GunDamage = PistolM.BulletDamage;
            GunShotDelay = PistolM.ShotDelay;
            playControl.SetShotDelay(GunShotDelay);
        }
    }

    public void SetGetItem(ItemManage.ITEMTYPE _itemType)
    {
        ItemType = _itemType;
    }

    public void SetHp(float _hp)
    {
        MaxHp = _hp;
    }
    void SetGetDamage(float _damage)
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
            if (GetHit)
            {
                SetGetDamage(70f);
            }
            if (Damage != 0)
            {
                curHp -= Damage;
                ResetDamage();
            }

            if (playControl.isGetItem)
            {
                UseItem = false;
                ItemType = playControl.GetItemType;
                if (ItemType == ItemManage.ITEMTYPE.HEAL && !UseItem)
                {
                    curHp += 50f;
                    UseItem = true;
                }
            }
        }
        else
            isDead = true;
    }

    void ConditionUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetHit = (HeadCheck.isHit || BodyCheck_1.isHit || BodyCheck_2.isHit ||
                  RightLegCheck_1.isHit || RightLegCheck_2.isHit ||
                  LeftLegCheck_1.isHit || LeftLegCheck_2.isHit);
        ATKUpdate();
        HpUpdate();
        ConditionUpdate();
     }
}
