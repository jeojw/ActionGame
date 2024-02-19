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
    public GameObject StopImage;

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        StopImage.SetActive(false);
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
                StopImage.SetActive(false);
            }
        }
    }

    public void ReturnStart()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Yes")
            {
                SceneManager.LoadScene("StartScene");
                Time.timeScale = 1;
                isPaused = false;
                StopImage.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            isPaused = true;
            StopImage.SetActive(true);
            Time.timeScale = 0f;
        }
        GameProgress();
    }
}
