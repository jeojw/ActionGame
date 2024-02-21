using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGame : MonoBehaviour
{
    public GameObject Player;
    List<GameObject> EnemyList;
    public List<Enemy_StatManage> EnemyStatList;
    public List<Transform> EnemyPosList;
    public List<Enemy_Movement> EnemyMovementList;
    public List<bool> DeadboolList;
    GameObject Enemy;
    public GameObject SceneUI;
    MainSceneManage MSM;
    public bool isReset;
    // Start is called before the first frame update
    void Start()
    {
        EnemyList = new List<GameObject>();
        Enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        MSM = SceneUI.GetComponent<MainSceneManage>();
        if (!isReset)
        {
            SetObjects();
        }
        else
        {
            ResetGame();
            isReset = false;
        }
            
    }

    public void Clear()
    {
        EnemyList.Clear();
        EnemyMovementList.Clear();
        EnemyStatList.Clear();
        EnemyMovementList.Clear();
    }
    public void SetObjects()
    {
        EnemyList.Add(Instantiate(Enemy, new Vector3(30f, 1f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(81f, 24f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(94, 24f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(225f, 18f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(282f, 18f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(345f, 50f, 147.699f), Quaternion.identity));
        EnemyList.Add(Instantiate(Enemy, new Vector3(360f, 50f, 147.699f), Quaternion.identity));

        EnemyList[0].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[1].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[2].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[3].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        EnemyList[4].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        EnemyList[5].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.RIFLE);
        EnemyList[6].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);

        for (int i = 0; i < EnemyList.Count; i++)
        {
            DeadboolList.Add(EnemyList[i].GetComponent<Enemy_StatManage>().isDead);
            EnemyStatList.Add(EnemyList[i].GetComponent<Enemy_StatManage>());
            EnemyPosList.Add(EnemyList[i].GetComponent<Transform>());
            EnemyMovementList.Add(EnemyList[i].GetComponent<Enemy_Movement>());
        }
    }

    public void ResetGame()
    {
        isReset = true;
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReset)
            isReset = false;
    }
}
