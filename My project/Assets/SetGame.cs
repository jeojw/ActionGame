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
        SetEnemy();
    }

    void SetEnemy()
    {
        EnemyList.Add(Instantiate(Enemy, new Vector3(30f, 1f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(81f, 24f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(94, 24f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(225f, 18f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(282f, 18f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(345f, 50f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(360f, 50f, 147.699f), Quaternion.identity));

        for (int i = 0; i < EnemyList.Count; i++)
        {
            EnemyStatList.Add(EnemyList[i].GetComponent<Enemy_StatManage>());
            EnemyPosList.Add(EnemyList[i].GetComponent<Transform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
