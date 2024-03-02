using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SceneUI : MonoBehaviour
{
    public GameObject Player;
    public GameObject Event;

    DataManage DataM;
    ScoreManage ScoreM;
    SetGame SG;

    public Image GuideImage;
    public TextMeshProUGUI text_Guide;
    RectTransform GuidePos;

    public TextMeshProUGUI text_Timer;
    public TextMeshProUGUI text_Score;
    public TextMeshProUGUI text_ResultScore;
    public TextMeshProUGUI text_ResultTimer;
    private float time_start;
    private float time_current;
    private bool isStored = false;

    protected float curHealth;
    private float MaxHealth;
    private float Score;

    PlayerControl.Weapons weapon;
    public Image None;
    public Image Pistol;
    public Image Rifle;
    public Image Knife;
    public Image Rope;

    public Image Pistol_Ammunition;
    public Image Pistol_Magazine;
    public Image Rifle_Ammunition;
    public TextMeshProUGUI Pistol_Ammunition_Text;
    public TextMeshProUGUI Pistol_Magazine_Text;
    public TextMeshProUGUI Rifle_Ammunition_Text;

    public TextMeshProUGUI ScoreText;

    public Slider HpBar;
    StatManage StatM;
    PlayerControl playControl;
    // Start is called before the first frame update

    void Start()
    {
        GuidePos = GuideImage.GetComponent<RectTransform>();
        DataM = Event.GetComponent<DataManage>();
        ScoreM = Event.GetComponent<ScoreManage>();
        SG = Event.GetComponent<SetGame>();
        MaxHealth = Player.GetComponent<StatManage>().MaxHp;

        Pistol.gameObject.SetActive(true);
        Pistol_Ammunition.gameObject.SetActive(true);
        Pistol_Ammunition_Text.gameObject.SetActive(true);

        Rifle.gameObject.SetActive(false);
        Rifle_Ammunition.gameObject.SetActive(false);
        Rifle_Ammunition_Text.gameObject.SetActive(false);

        Knife.gameObject.SetActive(false);

        Rope.gameObject.SetActive(false);

        StatM = Player.GetComponent<StatManage>();
        playControl = Player.GetComponent<PlayerControl>();

        Reset_Timer();
    }


    void StoreRecord()
    {
        DataM.MakeRecord(Score, time_current);
        DataM.StoreRecord();
    }

    private void UpdateScore()
    {
        Score = ScoreM.score;
        text_Score.text = Score.ToString();
    }

    private void ResultScore()
    {
        text_ResultScore.text = Score.ToString() + " pt";
    }

    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
        text_Timer.text = $"{time_current:N2} sec";
    }

    private void Check_Timer()
    {
        time_current = Time.time - time_start;
        text_Timer.text = $"{time_current:N2} sec";
    }

    private void End_Timer()
    {
        text_Timer.text = $"{time_current:N2} sec";
        text_ResultTimer.text = $"{time_current:N2} sec";
    }

    void GuideUI()
    {
        Vector3 CameraPos = Camera.main.transform.position;
        if (CameraPos.x >= 276 && CameraPos.x <= 314 && CameraPos.y < 52)
        {
            GuideImage.gameObject.SetActive(true);
            text_Guide.gameObject.SetActive(true);
        }

        else if (CameraPos.x >= 491 && CameraPos.x <= 532 && CameraPos.y < 59)
        {
            GuideImage.gameObject.SetActive(true);
            text_Guide.gameObject.SetActive(true);
        }
        else
        {
            GuideImage.gameObject.SetActive(false);
            text_Guide.gameObject.SetActive(false);
        }

        if (playControl.weapon == PlayerControl.Weapons.PISTOL ||
            playControl.weapon == PlayerControl.Weapons.RIFLE)
        {
            GuidePos.anchoredPosition = new Vector3(541.1f, 99.9f, 0f);
        }
        else
        {
            GuidePos.anchoredPosition = new Vector3(541.1f, 153f, 0f);
        }
    }

    void WeaponUI()
    {
        weapon = playControl.weapon;
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
                Pistol_Magazine.gameObject.SetActive(false);
                Pistol_Magazine_Text.gameObject.SetActive(false);
            }

            if (Rifle.gameObject != null &&
                Rifle_Ammunition.gameObject != null &&
                Rifle_Ammunition_Text.gameObject != null)
            {
                Rifle.gameObject.SetActive(false);
                Rifle_Ammunition.gameObject.SetActive(false);
                Rifle_Ammunition_Text.gameObject.SetActive(false);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == PlayerControl.Weapons.PISTOL)
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
                Pistol_Magazine.gameObject.SetActive(true);
                Pistol_Magazine_Text.gameObject.SetActive(true);
            }
            if (Rifle.gameObject != null &&
                Rifle_Ammunition.gameObject != null &&
                Rifle_Ammunition_Text.gameObject != null)
            {
                Rifle.gameObject.SetActive(false);
                Rifle_Ammunition.gameObject.SetActive(false);
                Rifle_Ammunition_Text.gameObject.SetActive(false);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(false);
        }
        else if (weapon == PlayerControl.Weapons.RIFLE)
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
                Pistol_Magazine.gameObject.SetActive(false);
                Pistol_Magazine_Text.gameObject.SetActive(false);
            }
            if (Rifle.gameObject != null &&
                Rifle_Ammunition.gameObject != null &&
                Rifle_Ammunition_Text.gameObject != null)
            {
                Rifle.gameObject.SetActive(true);
                Rifle_Ammunition.gameObject.SetActive(true);
                Rifle_Ammunition_Text.gameObject.SetActive(true);
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
                Pistol_Magazine.gameObject.SetActive(false);
                Pistol_Magazine_Text.gameObject.SetActive(false);
            }
            if (Rifle.gameObject != null &&
                Rifle_Ammunition.gameObject != null &&
                Rifle_Ammunition_Text.gameObject != null)
            {
                Rifle.gameObject.SetActive(false);
                Rifle_Ammunition.gameObject.SetActive(false);
                Rifle_Ammunition_Text.gameObject.SetActive(false);
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
                Pistol_Magazine.gameObject.SetActive(false);
                Pistol_Magazine_Text.gameObject.SetActive(false);
            }
            if (Rifle.gameObject != null &&
                Rifle_Ammunition.gameObject != null &&
                Rifle_Ammunition_Text.gameObject != null)
            {
                Rifle.gameObject.SetActive(false);
                Rifle_Ammunition.gameObject.SetActive(false);
                Rifle_Ammunition_Text.gameObject.SetActive(false);
            }

            if (Knife.gameObject != null)
                Knife.gameObject.SetActive(false);
            if (Rope.gameObject != null)
                Rope.gameObject.SetActive(true);
        }
    }
    void CheckHp()
    {
        curHealth = StatM.curHp;
        if (HpBar != null)
        {
            HpBar.value = curHealth / MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SG.isReset)
        {
            Reset_Timer();
            isStored = false;   
        }
        if (SG.isClear && !isStored)
        {
            StoreRecord();
            isStored = true;
        }

        GuideUI();
        WeaponUI();
        Check_Timer();
        CheckHp();
        UpdateScore();
        ResultScore();
        End_Timer();
    }
}
