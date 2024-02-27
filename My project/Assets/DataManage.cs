using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Profiling;
using UnityEngine;

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
}
public class Record
{
    public float _Score;
    public float _Time;
    public float ReturnScore() { return _Score; }
    public float ReturnTime() { return _Time; }

}
public class DataManage : MonoBehaviour
{
    RecordList recordList = new RecordList();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MakeRecord(float _score, float _time)
    {
        Record record = new Record();
        record._Score = _score;
        record._Time = _time;

        recordList.RecordStore(record);
    }

    public void StoreRecord()
    {
        File.WriteAllText(Application.dataPath + "/Records.json", JsonUtility.ToJson(recordList));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
