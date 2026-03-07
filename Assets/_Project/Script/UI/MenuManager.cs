using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] score;
    GameManager gameManager;


    void Start()
    {
        SetScore();
        gameManager = FindFirstObjectByType<GameManager>();
    }


    void Update()
    {

    }


    void SetScore()
    {
        if (score != null && score.Length >= 2)
        {
            score[0].text = "Score: \n" + GameManager.score;
            score[1].text = "highest Score: \n" + PlayerPrefs.GetFloat("HScore");
        }
    }


    public void Exit()
    {
        Application.Quit();
    }


    public void Return()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
        gameManager.TogglePause();
    }


    public void Restart()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene("GameScreen");
    }

    public void Main()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene("HomeScreen");
    }


    public void Setting()
    {
        SceneManager.LoadScene("SettingScreen");
    }
}