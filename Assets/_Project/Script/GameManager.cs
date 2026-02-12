using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip gameLoop;
    [SerializeField] AudioClip gameOver;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject ovni;
    AudioSource audioSource;
    public static EnemyManager[] enemies;
    PointManager pointManager;
    LifeManager lifeManager;
    public static float score;
    string actualSceneName = "Start";
    bool isCounting = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemies = GetEnemies();
        pointManager = GetComponent<PointManager>();
        lifeManager = GetComponent<LifeManager>();
        audioSource.loop = true;

    }

    void Update()
    {
        LoopManager();

        if (!isCounting)
        {
            isCounting = true;
            StartCoroutine(Counter());
        }
    }

    void LoopManager()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Game" && actualSceneName != "Game")
        {
            PlayOnGame(gameLoop);
            actualSceneName = "Game";
        }
        if (sceneName == "GameOver" && actualSceneName != "GameOver")
        {
            PlayOnGame(gameOver);
            actualSceneName = "GameOver";
        }
    }
    void PlayOnGame(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayOnce(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public EnemyManager[] GetEnemies()
    {
        return FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
    }

    public void SetEnemy()
    {
        enemies = GetEnemies();
    }

    public void IncreaseScore(string tag)
    {
        pointManager.IncreaseScore(tag);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        score = pointManager.GetScore();
        if (PlayerPrefs.GetFloat("HScore", 0) == 0 || score > PlayerPrefs.GetFloat("HScore", 0))
        {
            PlayerPrefs.SetFloat("HScore", score);
        }
        SceneManager.LoadScene("GameOver");
    }

    public void RestarVida()
    {
        lifeManager.RestarVida();
    }

    public void PopUpOvni()
    {
        GameObject newOvni = Instantiate(ovni, spawn.transform);
        newOvni.transform.localPosition = Vector3.zero;
    }

    IEnumerator Counter()
    {
                PopUpOvni();

        yield return new WaitForSecondsRealtime(21f);
        isCounting = false;
    }
}
