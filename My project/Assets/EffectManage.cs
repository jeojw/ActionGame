using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManage : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playAura = false;
    public ParticleSystem particleObject;
    public GameObject Player;
    public bool isShot;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isShot = Player.GetComponent<Movement>().isShooting;
        if (isShot)
            playAura = true;
        else
            playAura = false;

        if (playAura)
        {
            particleObject.Play();
        }
        else
            particleObject.Stop();
    }
}
