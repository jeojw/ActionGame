using System.Collections;
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
    GameObject Item;
    GameObject DropItem;
    List<Enemy_StatManage> EnemyStatList;
    List<Transform> EnemyPosList;

    float RifleRate = 0.1f;
    float HealRate = 0.1f;
    float PistolRate = 0.4f;

    bool isDropItem = false;

    string Path = "Prefabs/Items";
    // Start is called before the first frame update
    void Start()
    {
        playerControl = Player.GetComponent<PlayerControl>();
        EnemyStatList = GetComponent<SetGame>().EnemyStatList;
        EnemyPosList = GetComponent<SetGame>().EnemyPosList;
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

    public ITEMTYPE ReturnItem()
    {
        return ItemType;
    }

    private void Update()
    {
        //for (int i = 0; i < EnemyStatList.Count; i++)
        //{
        //    if (EnemyStatList[i].isDead && EnemyStatList[i].isDrop && !isDropItem)
        //    {
        //        ChooseItem();
        //        if (DropItem != null) {
        //            DropItem = Instantiate(DropItem, EnemyPosList[i].position - new Vector3(10, 0, 0), Quaternion.identity);
        //            isDropItem = true;
        //        }

        //    }
        //}

        if (!isDropItem)
        {
            DropItem = Instantiate(Item.transform.GetChild(1).gameObject, new Vector3(-4f, -0.4f, -0.1751058f), Quaternion.identity);
            isDropItem = true;
        }
        

        if (playerControl.CheckGetItem() && DropItem != null)
        {
            Destroy(DropItem);
        }
            
    }

}
