using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] score;
    void Start()
    {
        SetScore();
    }

    void Update()
    {

    }

    void SetScore()
    {
        score[0].text = "Score: " + GameManager.score;
        score[1].text = "highest Score: " + PlayerPrefs.GetFloat("HScore");
    }
}
