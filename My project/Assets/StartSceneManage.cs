using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManage : MonoBehaviour
{
    AudioSource ClickSound;

    private void Start()
    {
        ClickSound = transform.parent.GetChild(0).GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void Tutorial()
    {
        ClickSound.Play();
        SceneManager.LoadScene("Tutorial");
    }

    public void StartGame()
    {
        ClickSound.Play();
        SceneManager.LoadScene("MainScene");
    }

    public void Record()
    {
        ClickSound.Play();
        SceneManager.LoadScene("RecordScene");
    }

    public void ReturnStart()
    {
        ClickSound.Play();
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        ClickSound.Play();
        Application.Quit();
    }
}
