
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

    float RifleRate = 0.1f;
    float HealRate = 0.1f;
    float PistolRate = 0.3f;

    bool isDropItem = false;

    string Path = "Prefabs/Items";
    // Start is called before the first frame update
    void Start()
    {
        TypeList = new List<ITEMTYPE>();
        ItemList= new List<GameObject>();
        statManage = Player.GetComponent<StatManage>();
        playerControl = Player.GetComponent<PlayerControl>();
        EnemyStatList = GetComponent<SetGame>().EnemyStatList;
        EnemyPosList = GetComponent<SetGame>().EnemyPosList;
        IsDropList = GetComponent<SetGame>().isDropItem;
        Item = Resources.Load<GameObject>(Path);
    }

    void ChooseItem()
    {
        float RandomCoef = 10000f;
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
                playerControl.SetGetItem(ItemType);
                if (DropItem != null)
                {
                    DropItem = Instantiate(DropItem, EnemyPosList[i].position, Quaternion.identity);
                    ItemList.Add(DropItem);
                    TypeList.Add(ItemType);
                    IsDropList[i] = true;
                }

            }
        }
        
        if (ItemList.Count > 0)
        {
            for (int i = 0; i < ItemList.Count;i++) {
                if (playerControl.CheckGetItem() && ItemList[i] != null)
                {
                    GameObject DeleteItem = ItemList[i];
                    statManage.SetGetItem(TypeList[i]);
                    ItemList[i] = null;
                    Destroy(DeleteItem);
                }
            }
        }
    }

}
