using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Record()
    {
        SceneManager.LoadScene("RecordScene");
    }

    public void ReturnStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
