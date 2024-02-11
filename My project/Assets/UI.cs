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
    public GameObject Player;

    public TextMeshProUGUI text_Timer;
    private float time_start;
    private float time_current;
    private bool isEnded;
    
    protected float curHealth;
    public float MaxHealth;

    PlayerControl.Weapons weapon;
    public Image None;
    public Image Pistol;
    public Image Rifle;
    public MeshRenderer Knife;
    public Image Rope;

    public Image Pistol_Ammunition;
    public Image Rifle_Ammunition;
    public TextMeshProUGUI Pistol_Ammunition_Text;
    public TextMeshProUGUI Rifle_Ammunition_Text;

    public TextMeshProUGUI ScoreText;

    public Slider HpBar;
    // Start is called before the first frame update

    void Start()
    {
        MaxHealth = Player.GetComponent<StatManage>().MaxHp;

        Pistol.gameObject.SetActive(true);
        Pistol_Ammunition.gameObject.SetActive(true);
        Pistol_Ammunition_Text.gameObject.SetActive(true);

        Rifle.gameObject.SetActive(false);
        Rifle_Ammunition.gameObject.SetActive(false);
        Rifle_Ammunition_Text.gameObject.SetActive(false);

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
        weapon = GameObject.Find("Player").GetComponent<PlayerControl>().weapon;
        if (weapon == PlayerControl.Weapons.NONE)
        {
            if (None.gameObject != null)
                None.gameObject.SetActive(true);
            if (Pistol.gameObject != null &&
                Pistol_Ammunition.gameObject != null &&
                Pistol_Ammunition_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(false);
                Pistol_Ammunition.gameObject.SetActive(false);
                Pistol_Ammunition_Text.gameObject.SetActive(false);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == PlayerControl.Weapons.GUNS)
        {
            if (None.gameObject != null)
                None.gameObject.SetActive(false);
            if (Pistol.gameObject != null &&
                Pistol_Ammunition.gameObject != null &&
                Pistol_Ammunition_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(true);
                Pistol_Ammunition.gameObject.SetActive(true);
                Pistol_Ammunition_Text.gameObject.SetActive(true);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == PlayerControl.Weapons.KNIFE)
        {
            if (None.gameObject != null)
                None.gameObject.SetActive(false);
            if (Pistol.gameObject != null &&
                Pistol_Ammunition.gameObject != null &&
                Pistol_Ammunition_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(false);
                Pistol_Ammunition.gameObject.SetActive(false);
                Pistol_Ammunition_Text.gameObject.SetActive(false);
            }
            if (Knife.gameObject != null)
            {
                Knife.gameObject.SetActive(true);
                Knife.enabled = true;
            }
                
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == PlayerControl.Weapons.ROPE)
        {
            if (None.gameObject != null)
                None.gameObject.SetActive(false);
            if (Pistol.gameObject != null &&
                 Pistol_Ammunition.gameObject != null &&
                 Pistol_Ammunition_Text.gameObject != null)
            {
                Pistol.gameObject.SetActive(false);
                Pistol_Ammunition.gameObject.SetActive(false);
                Pistol_Ammunition_Text.gameObject.SetActive(false);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(true);
        }
    }
    public void CheckHp()
    {
        curHealth = Player.GetComponent<StatManage>().curHp;
        if (HpBar != null) {
            HpBar.value = curHealth / MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        WeaponUI();
        Check_Timer();
        CheckHp();
    }
}
