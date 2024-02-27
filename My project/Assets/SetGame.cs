using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SetGame : MonoBehaviour
{
    public GameObject Player;
    PlayerControl PC;
    public List<GameObject> EnemyList;
    public List<Enemy_StatManage> EnemyStatList;
    public List<Transform> EnemyPosList;
    public List<Enemy_Movement> EnemyMovementList;
    public List<bool> DeadboolList;
    GameObject Enemy;
    public GameObject SceneUI;
    ItemManage ITM;
    ScoreManage ScoreM;
    ProjectilesManage PTM;
    public bool isReset;
    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        PTM = GetComponent<ProjectilesManage>();
        ITM = GetComponent<ItemManage>();
        ScoreM = GetComponent<ScoreManage>();   
        EnemyList = new List<GameObject>();
        Enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        isClear = false;
        PC = Player.GetComponent<PlayerControl>();
        SetObjects();
            
    }
    void GameClear()
    {
        for (int i = 0; i < DeadboolList.Count; i++)
        {

        }
    }
    void ObjectClear()
    {
        EnemyList.Clear();
        EnemyMovementList.Clear();
        EnemyStatList.Clear();
        EnemyMovementList.Clear();
        DeadboolList.Clear();
    }
    public void SetObjects()
    {
        EnemyList.Add(Instantiate(Enemy, new Vector3(30f, 1f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(81f, 24f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(94, 24f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(260f, 18f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(270f, 18f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(280f, 18f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(290f, 18f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(345f, 50f, 147.699f), Quaternion.identity));
        //EnemyList.Add(Instantiate(Enemy, new Vector3(360f, 50f, 147.699f), Quaternion.identity));

        EnemyList[0].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        //EnemyList[1].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        //EnemyList[2].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        //EnemyList[3].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        //EnemyList[4].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        //EnemyList[5].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.RIFLE);
        //EnemyList[6].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.RIFLE);
        //EnemyList[7].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        //EnemyList[8].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);

        for (int i = 0; i < EnemyList.Count; i++)
        {
            DeadboolList.Add(false);
            EnemyStatList.Add(EnemyList[i].GetComponent<Enemy_StatManage>());
            EnemyPosList.Add(EnemyList[i].GetComponent<Transform>());
            EnemyMovementList.Add(EnemyList[i].GetComponent<Enemy_Movement>());
        }
    }
        
    public void ResetGame()
    {
        isReset = true;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        int NotDeadCount = 0;
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (DeadboolList[i])
                continue;
            DeadboolList[i] = EnemyStatList[i].isDead;   
            NotDeadCount++;
        }
        isClear = (NotDeadCount == 0);

        if (isReset)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Destroy(EnemyList[i]);
            }
            PTM.ResetList();
            ITM.ResetItemList();
            PC.ResetPlayer();
            ObjectClear();
            ScoreM.ResetScore();
            SetObjects();
            isReset = false;
            isClear = false;
        }
            
    }
}
