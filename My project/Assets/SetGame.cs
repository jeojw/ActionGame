using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGame : MonoBehaviour
{
    List<GameObject> EnemyList;
    public List<Enemy_StatManage> EnemyStatList;
    public List<Transform> EnemyPosList;
    GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        EnemyList = new List<GameObject>();
        Enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        EnemyList.Add(Instantiate(Enemy, new Vector3(33.25f, -1.8f, 147.699f), Quaternion.identity));
        EnemyStatList.Add(EnemyList[0].GetComponent<Enemy_StatManage>());
        EnemyPosList.Add(EnemyList[0].GetComponent<Transform>());
    }


    // Update is called once per frame
    void Update()
    {
    }
}
