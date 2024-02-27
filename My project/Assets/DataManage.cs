using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEditor;

public class RecordList
{
    public int Count;
    public List<float> Scorerecords = new List<float>();
    public List<float> Timerecords = new List<float>();

    public void RecordStore(Record _record)
    {
        this.Scorerecords.Insert(0, _record.ReturnScore());
        this.Timerecords.Insert(0, _record.ReturnTime());
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MakeRecord(float _score, float _time)
    {
        record = new Record(_score, _time);
    }

    void ListSort(RecordList _recordList)
    {
        for (int i = 0; i < _recordList.Count - 1; i++)
        {
            for (int j = i + 1; j < _recordList.Count; j++)
            {
                if (_recordList.Scorerecords[i] < _recordList.Scorerecords[j])
                {
                    float tmp = _recordList.Scorerecords[i];
                    _recordList.Scorerecords[i] = _recordList.Scorerecords[j];
                    _recordList.Scorerecords[j] = tmp;

                    float tmp2 = _recordList.Timerecords[i];
                    _recordList.Timerecords[i] = _recordList.Timerecords[j];
                    _recordList.Timerecords[j] = tmp2;
                }
                else if (_recordList.Scorerecords[i] == _recordList.Scorerecords[j])
                {
                    if (_recordList.Timerecords[i] > _recordList.Timerecords[j])
                    {
                        float tmp = _recordList.Scorerecords[i];
                        _recordList.Scorerecords[i] = _recordList.Scorerecords[j];
                        _recordList.Scorerecords[j] = tmp;

                        float tmp2 = _recordList.Timerecords[i];
                        _recordList.Timerecords[i] = _recordList.Timerecords[j];
                        _recordList.Timerecords[j] = tmp2;
                    }
                    else if (_recordList.Timerecords[i] == _recordList.Timerecords[j])
                    {
                        _recordList.RecordDelete(j);
                    }
                }
            }
        }
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
                ListSort(RecordList);
            }
            else
            {
                RecordList.RecordDelete(RecordList.Count - 1);
                RecordList.RecordStore(record);
                ListSort(RecordList);
            }
            File.WriteAllText(Application.dataPath + "/Records.json", JsonUtility.ToJson(RecordList));
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
