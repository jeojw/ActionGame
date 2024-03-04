using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SetGame : MonoBehaviour
{
    public GameObject Player;
    public GameObject CrackedGround;
    GroundManage GM;
    PlayerControl PC;
    public List<GameObject> EnemyList;
    public List<Enemy_StatManage> EnemyStatList;
    public List<Transform> EnemyPosList;
    public List<Enemy_Movement> EnemyMovementList;
    public List<bool> isDropItem;
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
        GM = CrackedGround.GetComponent<GroundManage>();
        PTM = GetComponent<ProjectilesManage>();
        ITM = GetComponent<ItemManage>();
        ScoreM = GetComponent<ScoreManage>();   
        EnemyList = new List<GameObject>();
        Enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        isClear = false;
        PC = Player.GetComponent<PlayerControl>();
        SetObjects();
            
    }
    void ObjectClear()
    {
        EnemyList.Clear();
        EnemyMovementList.Clear();
        EnemyStatList.Clear();
        DeadboolList.Clear();
        isDropItem.Clear();
    }
    public void SetObjects()
    {
        EnemyList = new List<GameObject>();
        EnemyStatList = new List<Enemy_StatManage>();
        EnemyPosList = new List<Transform>();
        EnemyMovementList = new List<Enemy_Movement>();
        
        EnemyList.Add(Instantiate(Enemy, new Vector3(22f, 0.8f, 147.699f), Quaternion.identity)); //0
        EnemyList.Add(Instantiate(Enemy, new Vector3(55.9f, 15f, 147.699f), Quaternion.identity)); //1
        EnemyList.Add(Instantiate(Enemy, new Vector3(65.4f, 15f, 147.699f), Quaternion.identity)); //2
        EnemyList.Add(Instantiate(Enemy, new Vector3(238f, 9f, 147.699f), Quaternion.identity)); //3
        EnemyList.Add(Instantiate(Enemy, new Vector3(242f, 9f, 147.699f), Quaternion.identity)); //4
        EnemyList.Add(Instantiate(Enemy, new Vector3(246f, 9f, 147.699f), Quaternion.identity)); //5
        EnemyList.Add(Instantiate(Enemy, new Vector3(253f, 9f, 147.699f), Quaternion.identity)); //6
        EnemyList.Add(Instantiate(Enemy, new Vector3(310f, 40f, 147.699f), Quaternion.identity)); //7
        EnemyList.Add(Instantiate(Enemy, new Vector3(313f, 40f, 147.699f), Quaternion.identity)); //8
        EnemyList.Add(Instantiate(Enemy, new Vector3(367f, 9f, 147.699f), Quaternion.identity)); //9
        EnemyList.Add(Instantiate(Enemy, new Vector3(371f, 9f, 147.699f), Quaternion.identity)); //10
        EnemyList.Add(Instantiate(Enemy, new Vector3(405f, 9f, 147.699f), Quaternion.identity)); //11
        EnemyList.Add(Instantiate(Enemy, new Vector3(470f, 50f, 147.699f), Quaternion.identity)); //12
        EnemyList.Add(Instantiate(Enemy, new Vector3(475f, 50f, 147.699f), Quaternion.identity)); //13

        EnemyList[0].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[1].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[2].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[3].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        EnemyList[4].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[5].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        EnemyList[6].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.RIFLE);
        EnemyList[7].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[8].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        EnemyList[9].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.PISTOL);
        EnemyList[10].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[11].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.RIFLE);
        EnemyList[12].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);
        EnemyList[13].GetComponent<Enemy_Movement>().SetAttackType(Enemy_Movement.ATTACKTYPE.SWORD);


        for (int i = 0; i < EnemyList.Count; i++)
        {
            DeadboolList.Add(false);
            EnemyStatList.Add(EnemyList[i].GetComponent<Enemy_StatManage>());
            EnemyPosList.Add(EnemyList[i].GetComponent<Transform>());
            EnemyMovementList.Add(EnemyList[i].GetComponent<Enemy_Movement>());
            isDropItem.Add(false);
        }
        ITM.SetList(EnemyStatList, EnemyPosList, isDropItem);
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
                Destroy(EnemyStatList[i]);
                Destroy(EnemyPosList[i]);
                Destroy(EnemyMovementList[i]);
            }
            GM.Reset();
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
