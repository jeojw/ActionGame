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
    private bool isDead;

    SetGame SG;
    PlayerControl PC;
    StatManage StM;
    List<bool> DeadboolList;
    List<bool> GetScore;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
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
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (StM.GetHit)
        {
            score -= 100;
        }
        if (StM.isDead && !isDead)
        {
            score -= 1000;
            isDead = true;
        }
    }
}
