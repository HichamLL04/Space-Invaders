using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip gameLoop;
    [SerializeField] AudioClip gameOver;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject ovni;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float velocidad = 1f;
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject inGame;
    [SerializeField] CanvasGroup inGameCanvasGroup;
    AudioSource audioSource;
    PointManager pointManager;
    LifeManager lifeManager;
    EnemyBoxManager enemyBoxManager;
    EnemyMovement enemyMovement;
    BrickManager[] brickManagers;
    public static EnemyManager[] enemies;
    public static float score;
    public static bool isCounting = false;
    public static int wave = 1;
    string actualSceneName = "Start";
    string currentAudioScene = "";
    bool isPaused = false;
    bool isTogglingPause = false;



    void Start()
    {
        if (textMeshProUGUI != null)
            textMeshProUGUI.enabled = false;
        
        audioSource = GetComponent<AudioSource>();
        enemies = GetEnemies();
        pointManager = GetComponent<PointManager>();
        lifeManager = GetComponent<LifeManager>();
        enemyBoxManager = GetComponent<EnemyBoxManager>();
        enemyMovement = FindFirstObjectByType<EnemyMovement>();
        brickManagers = FindObjectsByType<BrickManager>(FindObjectsSortMode.None);
        
        if (audioSource != null)
            audioSource.loop = true;
        
        NextWave();
    }

    void Update()
    {
        LoopManager();

        if (!isCounting)
        {
            isCounting = true;
            StartCoroutine(GenerateOvni());
        }
    }

    void LoopManager()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName != currentAudioScene)
        {
            currentAudioScene = sceneName;
            
            if (sceneName == "Game" || sceneName == "HomeScreen" || sceneName == "SettingScreen")
            {
                PlayOnGame(gameLoop);
            }
            else if (sceneName == "GameOver")
            {
                PlayOnGame(gameOver);
            }
        }
        
        actualSceneName = sceneName;
    }

    void PlayOnGame(AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayOnce(AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        
        audioSource.PlayOneShot(audioClip);
    }

    public EnemyManager[] GetEnemies()
    {
        enemies = FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
        return enemies;
    }

    public void SetEnemy()
    {
        GetEnemies();
        if (enemies != null && enemies.Count() == 1)
        {
            wave++;
            NextWave();
        }
    }

    public int GetEnemyCount()
    {
        GetEnemies();
        return enemies?.Count() ?? 0;
    }

    public void IncreaseScore(string tag)
    {
        if (pointManager != null)
            pointManager.IncreaseScore(tag);
    }

    public void GameOver()
    {
        if (pointManager != null)
        {
            score = pointManager.GetScore();
            if (PlayerPrefs.GetFloat("HScore", 0) == 0 || score > PlayerPrefs.GetFloat("HScore", 0))
            {
                PlayerPrefs.SetFloat("HScore", score);
            }
        }
        SceneManager.LoadScene("GameOver");
    }

    public void RestarVida()
    {
        if (lifeManager != null)
            lifeManager.RestarVida();
    }

    public void PopUpOvni()
    {
        if (ovni != null && spawn != null)
        {
            GameObject newOvni = Instantiate(ovni, spawn.transform);
            newOvni.transform.localPosition = Vector3.zero;
        }
    }

    public float GetVelocidad()
    {
        return velocidad;
    }

    void NextWave()
    {
        StartCoroutine(GenerateEnemy());
    }

    IEnumerator GenerateEnemy()
    {
        yield return new WaitForSeconds(1);

        if (wave > 1)
        {
            if (brickManagers != null && brickManagers.Length > 0)
            {
                foreach (BrickManager brick in brickManagers)
                {
                    if (brick != null)
                        brick.EnableBrick();
                }
            }
            
            if (enemyMovement != null)
                enemyMovement.SetVelocidad(0.5f);
        }

        if (enemyMovement != null)
        {
            enemyMovement.ContVelocidad(false);
            enemyMovement.SetLocation();
        }
        
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = "STARTING WAVE " + wave;
            textMeshProUGUI.enabled = true;
        }
        
        yield return new WaitForSeconds(3);

        if (enemyBoxManager != null)
            enemyBoxManager.GenerateEnemy(wave);
        
        if (enemyMovement != null)
            enemyMovement.ContVelocidad(true);
        
        if (textMeshProUGUI != null)
            textMeshProUGUI.enabled = false;
    }

    IEnumerator GenerateOvni()
    {
        yield return new WaitForSeconds(21f);
        PopUpOvni();
    }

    public float GetClipLengh(string name, Animator animator)
    {
        if (animator == null)
            return 0f;
        
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name)
                return clip.length;
        }
        return 0f;
    }

    public void TogglePause()
    {
        if (isTogglingPause || menuPausa == null || inGame == null)
            return;

        isTogglingPause = true;
        isPaused = !isPaused;

        if (isPaused)
        {
            menuPausa.SetActive(true);
            Time.timeScale = 0f;
            inGame.SetActive(false);
        }
        else
        {
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
            inGame.SetActive(true);
        }

        isTogglingPause = false;
    }
}