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
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == "GameOver")
        {
            gameManager.TogglePause();
        }
        else if (currentScene == "SettingScreen")
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Main()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void Setting()
    {
        SceneManager.LoadScene("SettingScreen");
    }
}