using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class RecordManage : MonoBehaviour
{
    public GameObject Records;
    RecordList newRecords = new RecordList();

    List<Tuple<TextMeshProUGUI, TextMeshProUGUI>> SetRecordList = new List<Tuple<TextMeshProUGUI, TextMeshProUGUI>>();

    private float preScore = 0f;
    private float preTime = 0f;

    private float score;
    private float time;

    private bool isRenewal;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)     
        {
            SetRecordList.Add(new Tuple<TextMeshProUGUI, TextMeshProUGUI> (Records.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(),
                                                                           Records.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>()));
        }

        string file = File.ReadAllText(Application.dataPath + "/Records.json");
        newRecords = JsonUtility.FromJson<RecordList>(file);

        for (int i = 0; i < newRecords.Count; i++)
        {
            SetRecordList[i].Item1.text = newRecords.Scorerecords[i].ToString();
            SetRecordList[i].Item2.text = newRecords.Timerecords[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
