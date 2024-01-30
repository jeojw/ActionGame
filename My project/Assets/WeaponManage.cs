using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManage : MonoBehaviour
{
    // Start is called before the first frame update\

    public GameObject Player;
    public GameObject WeaponUI;

    public bool isReload;
    public bool MagazineZero;

    public int curMagazine;
    public int maxMagazine;

    public float Reload_Delay;
    void Start()
    {           
        curMagazine = maxMagazine;
    }

    void Shot()
    {

        if (Player.GetComponent<Movement>().delayElapsed == 0 &&
            Player.GetComponent<Movement>().isShooting)
        {
            if (curMagazine != 0)
            {
                curMagazine -= 1;
            }

            if (curMagazine == 0)
            {
                MagazineZero = true;
            }
        }

    }

    void Reload()
    {
        if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reloading_Pistol"))
        {
            if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                curMagazine = maxMagazine;
                MagazineZero = false;
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Shot();
        if (MagazineZero)
            Reload();
    }
}
