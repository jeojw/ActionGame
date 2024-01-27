using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject StopImage, Yes, No;

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        StopImage.gameObject.SetActive(false);
        Yes.gameObject.SetActive(false);
        No.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameProgress()
    {
        if (isPaused && EventSystem.current.currentSelectedGameObject != null)
        {
            isPaused = false;
            Time.timeScale = 1;
            StopImage.gameObject.SetActive(false);
            Yes.gameObject.SetActive(false);
            No.gameObject.SetActive(false);
        }
    }

    public void ReturnStart()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Yes")
            {
                SceneManager.LoadScene("StartScene");
            }

        }

    }

    void StopGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            Time.timeScale = 0;
            StopImage.gameObject.SetActive(true);
            Yes.gameObject.SetActive(true);
            No.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StopGame();
        ReturnStart();
        GameProgress();
    }
}
