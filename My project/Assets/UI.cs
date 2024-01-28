using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI text_Timer;
    private float time_start;
    private float time_current;
    private bool isEnded;
    
    protected float curHealth;
    public float MaxHealth;

    Movement.Weapons weapon;
    public Image Pistol;
    public Image Rifle;
    public MeshRenderer Knife;
    public Image Rope;

    public Image Pistol_Magazine;
    public Image Rifle_Magazine;
    public TextMeshProUGUI Pistol_Magazine_Text;
    public TextMeshProUGUI Rifle_Magazine_Text;

    public TextMeshProUGUI ScoreText;

    public Slider HpBar;
    // Start is called before the first frame update

    void Start()
    {
        Pistol.gameObject.SetActive(true);
        Pistol_Magazine.gameObject.SetActive(true);
        Pistol_Magazine_Text.gameObject.SetActive(true);

        Rifle.gameObject.SetActive(false);
        Rifle_Magazine .gameObject.SetActive(false);
        Rifle_Magazine_Text .gameObject.SetActive(false);

        Knife.gameObject.SetActive(false);

        Rope.gameObject.SetActive(false);

        Reset_Timer();
    }

    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
        text_Timer.text = $"{time_current:N2} sec";
        isEnded = false;
    }

    private void Check_Timer()
    {
        time_current = Time.time - time_start;
        text_Timer.text = $"{time_current:N2} sec";
    }

    private void End_Timer()
    {
        text_Timer.text = $"{time_current:N2} sec";
        isEnded = true;
    }
    
    void WeaponUI()
    {
        weapon = GameObject.Find("Player").GetComponent<Movement>().weapon;
        if (weapon == Movement.Weapons.GUNS)
        {
            if (Pistol.gameObject != null &&
                Pistol_Magazine.gameObject != null &&
                Pistol_Magazine_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(true);
                Pistol_Magazine.gameObject.SetActive(true);
                Pistol_Magazine_Text.gameObject.SetActive(true);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == Movement.Weapons.KNIFE)
        {
            if (Pistol.gameObject != null &&
                Pistol_Magazine.gameObject != null &&
                Pistol_Magazine_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(false);
                Pistol_Magazine.gameObject.SetActive(false);
                Pistol_Magazine_Text.gameObject.SetActive(false);
            }
            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(true);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == Movement.Weapons.ROPE)
        {
            if (Pistol.gameObject != null &&
                 Pistol_Magazine.gameObject != null &&
                 Pistol_Magazine_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(false);
                Pistol_Magazine.gameObject.SetActive(false);
                Pistol_Magazine_Text.gameObject.SetActive(false);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(true);
        }
    }


    
    public void SetHp(float account)
    {
        MaxHealth = account;
        curHealth = MaxHealth;
    }

    public void CheckHp()
    {
        if (HpBar != null) {
            HpBar.value = curHealth / MaxHealth;
        }
    }

    public void GetDemage(float damage)
    {
        if (MaxHealth == 0 || curHealth <= 0)
            return;
        curHealth -= damage;
        CheckHp();
        if (curHealth < 0) { 
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        WeaponUI();
        Check_Timer();
    }
}
