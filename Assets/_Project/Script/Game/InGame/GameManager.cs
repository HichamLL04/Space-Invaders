using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem;
using System;


public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject spawn;
    [SerializeField] GameObject ovni;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float velocidad = 1f;
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject inGame;
    [SerializeField] CanvasGroup inGameCanvasGroup;
    PointManager pointManager;
    AudioManager audioManager;
    LifeManager lifeManager;
    EnemyBoxManager enemyBoxManager;
    EnemyMovement enemyMovement;
    BrickManager[] brickManagers;
    PlayerMovement playerMovement;
    public static EnemyManager[] enemies;
    public static float score;
    public static bool isCounting = false;
    public static int wave = 1;
    bool isTogglingPause = false;
    public static bool isPaused = false;
    private float ovniTimer = 0f;


    void Start()
    {

        Time.timeScale = 1f;
        isPaused = false;
        isCounting = false;
        wave = 1;
        score = 0;

        if (textMeshProUGUI != null)
            textMeshProUGUI.enabled = false;

        enemies = GetEnemies();
        pointManager = GetComponent<PointManager>();
        lifeManager = GetComponent<LifeManager>();
        enemyBoxManager = GetComponent<EnemyBoxManager>();
        enemyMovement = FindFirstObjectByType<EnemyMovement>();
        brickManagers = FindObjectsByType<BrickManager>(FindObjectsSortMode.None);
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        audioManager = FindFirstObjectByType<AudioManager>();

        NextWave();
    }

    void Update()
    {
        if (!isPaused)
        {
            ovniTimer += Time.deltaTime;
            if (ovniTimer >= 21f)
            {
                PopUpOvni();
                ovniTimer = 0f;
            }
        }
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
                    {
                        brick.EnableBrick();
                    }
                    else
                    {
                        Debug.Log("Null");
                    }
                }
            }

            if (enemyMovement != null)
            {
                enemyMovement.SetVelocidad(0.05f);
                enemyMovement.SetProbabilidad(0.15f);
            }
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


    public void Hit()
    {
        playerMovement.Hit();
    }


    public void PlayClip(String clipName)
    {
        audioManager.PlayOnce(clipName);
    }
}