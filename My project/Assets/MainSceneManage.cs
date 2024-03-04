using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManage : MonoBehaviour
{
    public GameObject StopPopup;
    public GameObject DeadPopup;
    public GameObject ClearPopup;
    public GameObject Player;
    StatManage StatM;
    public GameObject Event;
    SetGame SG;
    public AudioSource ClickSound;
    public AudioSource StopSound;
    public AudioSource BGM;

    private bool _isPaused = false;
    private bool isReset = false;

    public bool isPaused { 
        get { return _isPaused; }
        set { _isPaused = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        StopPopup.SetActive(false);
        DeadPopup.SetActive(false);
        StatM = Player.GetComponent<StatManage>();
        SG = Event.GetComponent<SetGame>();
    }

    public void GameProgress()
    {
        if (_isPaused && EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "No")
            {
                ClickSound.Play();
                BGM.Play();
                Time.timeScale = 1;
                _isPaused = false;
                StopPopup.SetActive(false);
            }
        }
    }

    public void ReturnStart()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Yes" ||
                EventSystem.current.currentSelectedGameObject.name == "DNo" ||
                EventSystem.current.currentSelectedGameObject.name == "Quit")
            {
                ClickSound.Play();
                SceneManager.LoadScene("StartScene");
                Time.timeScale = 1;
                _isPaused = false;
                StopPopup.SetActive(false);
                DeadPopup.SetActive(false);
            }
        }
    }

    public void ResetGame()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "DYes" ||
                EventSystem.current.currentSelectedGameObject.name == "Retry")
            {
                ClickSound.Play();
                isReset = true;
                SG.ResetGame();
                StatM.ResetHp();
                DeadPopup.SetActive(false);
                ClearPopup.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !StopPopup.activeSelf)
            StopSound.Play();
        if (Input.GetKey(KeyCode.Escape))
        {
            BGM.Pause();
            StopPopup.SetActive(true);
            Time.timeScale = 0f;
        }

        _isPaused = StopPopup.activeSelf;

        if (StatM.isDead)
        {
            DeadPopup.SetActive(true);
        }
        else
            DeadPopup.SetActive(false);

        if (SG.isClear)
        {
            ClearPopup.SetActive(true);
            Time.timeScale = 0f;
        }

        else if (SG.isClear || !_isPaused)
        {
            ClearPopup.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
