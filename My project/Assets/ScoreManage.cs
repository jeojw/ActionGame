using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    public float score;
    public GameObject Player;
    private int DeadCount;
    private int PlayerDeath;
    private int PlayerGetHit;
    private int GetItem;
    private bool GetBonus = false;

    SetGame SG;
    PlayerControl PC;
    StatManage StM;
    List<bool> DeadboolList;
    List<bool> GetScore;

    // Start is called before the first frame update
    void Start()
    {
        SG = GetComponent<SetGame>();
        PC = Player.GetComponent<PlayerControl>();
        StM = Player.GetComponent<StatManage>();
        DeadboolList = SG.DeadboolList;
        GetScore = new List<bool> { false };
        GetScore = Enumerable.Repeat(false, DeadboolList.Count).ToList();
    }
    public void ResetScore()
    {
        score = 0;
        GetBonus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SG.isClear && !GetBonus)
        {
            score += StM.curHp * 2 + 300;
            GetBonus = true;
        }
        for (int i = 0; i < DeadboolList.Count; i++)    
        {
            if (DeadboolList[i] && !GetScore[i])
            {
                score += 200;
                GetScore[i] = true;
            }
        }
        if (PC.isGetItem)
            score += 30;
        if (StM.GetHit && !StM.isDead)
        {
            score -= 100;
        }
    }
}
