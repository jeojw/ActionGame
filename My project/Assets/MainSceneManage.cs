using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManage : MonoBehaviour
{
    public GameObject StopPopup;
    public GameObject DeadPopup;
    public GameObject Player;
    StatManage StatM;
    public GameObject Event;
    SetGame SG;

    public bool isPaused = false;
    public bool isReset = false;
    // Start is called before the first frame update
    void Start()
    {
        StopPopup.SetActive(false);
        DeadPopup.SetActive(false);
        StatM = Player.GetComponent<StatManage>();
        SG = Event.GetComponent<SetGame>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameProgress()
    {
        if (isPaused && EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "No")
            {
                Time.timeScale = 1;
                isPaused = false;
                StopPopup.SetActive(false);
            }
        }
    }

    public void ReturnStart()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Yes" ||
                EventSystem.current.currentSelectedGameObject.name == "DNo")
            {
                SceneManager.LoadScene("StartScene");
                Time.timeScale = 1;
                isPaused = false;
                StopPopup.SetActive(false);
                DeadPopup.SetActive(false);
            }
        }
    }

    public void ResetGame()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "DYes")
            {
                isReset = true;
                SG.ResetGame();
                StatM.ResetHp();
                DeadPopup.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            isPaused = true;
            StopPopup.SetActive(true);
            Time.timeScale = 0f;
        }

        if (StatM.isDead)
        {
            DeadPopup.SetActive(true);
        }
        else
            DeadPopup.SetActive(false);
        GameProgress();
    }
}
