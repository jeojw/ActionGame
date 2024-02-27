using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class RecordList
{
    public int Count;
    public List<float> Scorerecords = new List<float>();
    public List<float> Timerecords = new List<float>();

    public void RecordStore(Record _record)
    {
        this.Scorerecords.Add(_record.ReturnScore());
        this.Timerecords.Add(_record.ReturnTime());
        Count++;
    }

    public void RecordDelete(int idx)
    {
        this.Scorerecords.RemoveAt(idx);
        this.Timerecords.RemoveAt(idx);
        Count--;
    }
}
public class Record
{
    private float _Score;
    private float _Time;
    public Record(float _score, float _time) { _Score = _score; _Time = _time; }
    
    public float ReturnScore() { return _Score; }
    public float ReturnTime() { return _Time; }

}

public class DataManage : MonoBehaviour
{
    RecordList recordList = new RecordList();
    Record record;

    private
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MakeRecord(float _score, float _time)
    {
        record = new Record(_score, _time);
    }

    public void StoreRecord()
    {
        string file = File.ReadAllText(Application.dataPath + "/Records.json");
        var RecordList = JsonUtility.FromJson<RecordList>(file);
        if (RecordList != null)
        {
            if (RecordList.Count < 5) 
            {
                RecordList.RecordStore(record);
                for (int i = 0; i < RecordList.Count - 1; i++)
                {
                    for (int j = i + 1; j < RecordList.Count; j++)
                    {
                        if (RecordList.Scorerecords[i] < RecordList.Scorerecords[j])
                        {
                            float tmp = RecordList.Scorerecords[i];
                            RecordList.Scorerecords[i] = RecordList.Scorerecords[j];
                            RecordList.Scorerecords[j] = tmp;

                            float tmp2 = RecordList.Timerecords[i];
                            RecordList.Timerecords[i] = RecordList.Timerecords[j];
                            RecordList.Timerecords[j] = tmp2;
                        }
                        else if (RecordList.Scorerecords[i] == RecordList.Scorerecords[j])
                        {
                            if (RecordList.Timerecords[i] > RecordList.Timerecords[j])
                            {
                                float tmp = RecordList.Scorerecords[i];
                                RecordList.Scorerecords[i] = RecordList.Scorerecords[j];
                                RecordList.Scorerecords[j] = tmp;

                                float tmp2 = RecordList.Timerecords[i];
                                RecordList.Timerecords[i] = RecordList.Timerecords[j];
                                RecordList.Timerecords[j] = tmp2;
                            }
                            else if (RecordList.Timerecords[i] == RecordList.Timerecords[j])
                            {
                                RecordList.RecordDelete(j);
                            }
                        }
                    }
                }
                File.WriteAllText(Application.dataPath + "/Records.json", JsonUtility.ToJson(RecordList));
            }
            else
            {

            }
        }
        else
        {
            recordList.RecordStore(record);
            File.WriteAllText(Application.dataPath + "/Records.json", JsonUtility.ToJson(recordList));
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
