using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SceneUI : MonoBehaviour
{
    Vector3 HookPos;
    float HookAngle;

    public GameObject Player;
    public GameObject Event;

    DataManage DataM;
    ScoreManage ScoreM;
    SetGame SG;

    public Image HookImage;
    RectTransform HookImagePos;

    public Image DetectImage;
    RectTransform DetectPos;

    public Image GuideImage;
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

    float ScreenHeightHalf;
    float ScreenWidthHalf;

    PlayerControl.Weapons weapon;
    public Image None;
    public Image Pistol;
    public Image Rifle;
    public Image Knife;
    public Image Rope;

    public Image Pistol_Ammunition;
    public Image Pistol_Magazine;
    public Image Rifle_Ammunition;
    public Image Rifle_Magazine;
    public TextMeshProUGUI Pistol_Ammunition_Text;
    public TextMeshProUGUI Pistol_Magazine_Text;
    public TextMeshProUGUI Rifle_Ammunition_Text;
    public TextMeshProUGUI Rifle_Magazine_Text;

    public TextMeshProUGUI ScoreText;

    public Slider HpBar;
    StatManage StatM;
    PlayerControl playControl;
    // Start is called before the first frame update

    void Start()
    {
        ScreenHeightHalf = Camera.main.orthographicSize;
        ScreenWidthHalf = ScreenHeightHalf * Camera.main.aspect;

        GuidePos = GuideImage.GetComponent<RectTransform>();
        DetectPos = DetectImage.GetComponent<RectTransform>();
        HookImagePos = HookImage.GetComponent<RectTransform>();

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

    void DetectUI()
    {
        Vector3 CameraPos = Camera.main.transform.position;
        RaycastHit2D DetectHit = Physics2D.Raycast(CameraPos, new Vector2((int)playControl.direction, 0), 60f, LayerMask.GetMask("Enemy"), 20f);
        if (DetectHit.collider != null)
        {
            DetectImage.gameObject.SetActive(true);
        }
        else
            DetectImage.gameObject.SetActive(false);
        DetectPos.anchoredPosition = new Vector3(ScreenWidthHalf * 2 - DetectPos.rect.width * 1.2f, ScreenHeightHalf, 0f);
    }

    void HookUI()
    {
        HookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HookAngle = Mathf.Atan2(HookPos.y - Player.transform.position.y, HookPos.x - Player.transform.position.x) * Mathf.Rad2Deg;
        if (playControl.weapon == PlayerControl.Weapons.ROPE)
        {
            HookImage.gameObject.SetActive(true);

            HookImage.gameObject.transform.rotation = Quaternion.AngleAxis(HookAngle, Vector3.forward);
            HookImagePos.anchoredPosition = HookPos;
        }
        else
            HookImage.gameObject.SetActive(false);
    }

    void GuideUI()
    {
        Vector3 CameraPos = Camera.main.transform.position;
        
        if (CameraPos.x >= 251.8408f && CameraPos.x <= 278f && CameraPos.y < 35f)
        {
            GuideImage.gameObject.SetActive(true);
        }

        else if (CameraPos.x >= 430f && CameraPos.x <= 530f && CameraPos.y < 47)
        {
            GuideImage.gameObject.SetActive(true);
        }

        else
        {
            GuideImage.gameObject.SetActive(false);
        }

        GuidePos.anchoredPosition = new Vector3(ScreenWidthHalf * 2 - GuidePos.rect.width * 1.2f, ScreenHeightHalf * 2 - GuidePos.rect.height * 2, 0f);
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
                Rifle_Magazine.gameObject.SetActive(false);
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
                Rifle_Magazine.gameObject.SetActive(false);
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
                Rifle_Magazine.gameObject.SetActive(true);
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
                Rifle_Magazine.gameObject.SetActive(false);
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
                Rifle_Magazine.gameObject.SetActive(false);
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

        HookUI();
        GuideUI();
        DetectUI();
        WeaponUI();
        Check_Timer();
        CheckHp();
        UpdateScore();
        ResultScore();
        End_Timer();
    }
}
