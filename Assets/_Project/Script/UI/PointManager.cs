using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore(string tag)
    {
        switch (tag)
        {
            case "Enemy1":
                score += 5;
                break;
            case "Enemy2":
                score += 10;
                break;
            case "Enemy3":
                score += 20;
                break;
            case "Enemy4":
                score += 40;
                break;
            case "Enemy5":
                score += 50;
                break;
            case "Ovni":
                score += 100;
                break;
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        textMeshProUGUI.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
