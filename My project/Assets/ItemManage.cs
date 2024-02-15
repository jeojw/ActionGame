using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    public GameObject Player;
    PlayerControl playerControl;
    GameObject Item;
    GameObject DropItem;
    List<Enemy_StatManage> EnemyStatList;
    List<Transform> EnemyPosList;

    float WeaponRate = 0.3f;
    float NoneRate = 0.5f;
    float HealRate = 0.2f;

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
        float WeaponRange = RandomCoef * WeaponRate;
        float HealRange = RandomCoef * HealRate;

        float Rand = Random.Range(1, RandomCoef + 1);
        if (1 <= Rand && Rand <= WeaponRange)
            DropItem = Item.transform.GetChild(1).gameObject;
        else if (Rand > WeaponRange && Rand <= HealRange + WeaponRange)
            DropItem = Item.transform.GetChild(0).gameObject;
        else
            DropItem = null;
    }

    private void Update()
    {
        for (int i = 0; i < EnemyStatList.Count; i++)
        {
            if (EnemyStatList[i].isDead && EnemyStatList[i].isDrop && !isDropItem)
            {
                ChooseItem();
                if (DropItem != null) {
                    DropItem = Instantiate(DropItem, EnemyPosList[i].position - new Vector3(10, 0, 0), Quaternion.identity);
                    isDropItem = true;
                }
                
            }
        }

        if (playerControl.GetItem && DropItem != null)
            Destroy(DropItem);
    }

}
