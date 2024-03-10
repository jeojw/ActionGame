
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    public enum ITEMTYPE
    {
        RIFLE,
        PISTOL,
        HEAL,
        NONE
    }
    public ITEMTYPE ItemType;
    public GameObject Player;
    PlayerControl playerControl;
    StatManage statManage;
    GameObject Item;
    GameObject DropItem;
    List<Enemy_StatManage> EnemyStatList;
    List<Transform> EnemyPosList;
    List<ITEMTYPE> TypeList;
    List<GameObject> ItemList;
    List<bool> IsDropList;
    GameObject DeleteItem;

    readonly float RifleRate = 0.15f;
    readonly float HealRate = 0.2f;
    readonly float PistolRate = 0.4f;

    readonly string Path = "Prefabs/Items";
    // Start is called before the first frame update
    void Start()
    {
        TypeList = new List<ITEMTYPE>();
        ItemList= new List<GameObject>();
        statManage = Player.GetComponent<StatManage>();
        playerControl = Player.GetComponent<PlayerControl>();
        Item = Resources.Load<GameObject>(Path);
    }

    public void SetList(List<Enemy_StatManage> _ESL,
                        List<Transform> _ETL,
                        List<bool> _IsDropList)
    {
        EnemyStatList = _ESL;   EnemyPosList = _ETL;    IsDropList = _IsDropList;
    }

    void ChooseItem()
    {
        float RandomCoef = 100f;
        float RifleRange = RandomCoef * RifleRate;
        float PistolRange = RandomCoef * PistolRate;
        float HealRange = RandomCoef * HealRate;

        float Rand = Random.Range(1, RandomCoef + 1);
        if (1 <= Rand && Rand <= RifleRange)
        {
            DropItem = Item.transform.GetChild(1).gameObject;
            ItemType = ITEMTYPE.RIFLE;
        }

        else if (Rand > RifleRange && Rand <= HealRange + RifleRange)
        {
            DropItem = Item.transform.GetChild(0).gameObject;
            ItemType = ITEMTYPE.HEAL;
        }

        else if (Rand > HealRange + RifleRange && Rand <= HealRange + RifleRange + PistolRange)
        {
            DropItem = Item.transform.GetChild(2).gameObject;
            ItemType = ITEMTYPE.PISTOL;
        }

        else if (Rand > HealRange + RifleRange + PistolRange)
        {
            DropItem = null;
            ItemType = ITEMTYPE.NONE;
        }
    }

    public void ResetItemList()
    {
        for (int i = 0; i < ItemList.Count; i++)
            Destroy(ItemList[i]);
        ItemList.Clear();
    }

    private void Update()
    {
        for (int i = 0; i < EnemyStatList.Count; i++)
        {
            if (EnemyStatList[i].isDead && EnemyStatList[i].isDrop && !IsDropList[i])
            {
                ChooseItem();
                if (DropItem != null)
                {
                    DropItem = Instantiate(DropItem, EnemyPosList[i].position, Quaternion.identity);
                    ItemList.Add(DropItem);
                    TypeList.Add(ItemType);
                }
                IsDropList[i] = true;
            }
        }
        
        if (ItemList.Count > 0)
        {
            for (int i = ItemList.Count - 1; i >= 0; i--) {
                if (playerControl.isGetItem && ItemList[i] != null)
                {
                    DeleteItem = ItemList[i];
                    statManage.SetGetItem(TypeList[i]);
                    ItemList.Remove(ItemList[i]);
                    Destroy(DeleteItem);
                }
            }
        }
    }

}
