using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float score;
    public GameObject Event;
    SetGame SG;
    List<bool> DeadboolList;

    // Start is called before the first frame update
    void Start()
    {
        SG = Event.GetComponent<SetGame>();
        DeadboolList = SG.DeadboolList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
