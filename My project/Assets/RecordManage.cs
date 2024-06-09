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

    List<TextMeshProUGUI> Rank = new List<TextMeshProUGUI>();
    List<Tuple<TextMeshProUGUI, TextMeshProUGUI>> SetRecordList = new List<Tuple<TextMeshProUGUI, TextMeshProUGUI>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)     
        {
            Rank.Add(Records.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
            SetRecordList.Add(new Tuple<TextMeshProUGUI, TextMeshProUGUI> (Records.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(),
                                                                           Records.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>()));
        }
        
        if (File.Exists(Application.persistentDataPath + "/Records.json"))
        {
            string file = File.ReadAllText(Application.persistentDataPath + "/Records.json");
            newRecords = JsonUtility.FromJson<RecordList>(file);

            for (int i = 0; i < newRecords.Count; i++)
            {
                if (SetRecordList[i].Item1 != null)
                {
                    Rank[i].text = (i + 1).ToString() + ".";
                }
                SetRecordList[i].Item1.text = newRecords.Scorerecords[i].ToString();
                SetRecordList[i].Item2.text = newRecords.Timerecords[i].ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
