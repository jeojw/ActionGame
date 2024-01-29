using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProgress : MonoBehaviour
{
    private List<GameObject> PlayerBulletList = new List<GameObject>();
    private int MaxMagazine;
    // Start is called before the first frame update
    void Start()
    {
        MaxMagazine = 6;
    }

    void ChangeWeapon()
    {
        
    }
    void BulletAdd(GameObject obj)
    {
        if (PlayerBulletList.Count <= MaxMagazine)
            PlayerBulletList.Add(obj);
    }

    void BulletDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
