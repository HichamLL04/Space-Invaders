using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    public static EnemyManager[] enemies;
    PointManager pointManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemies = GetEnemies();
        pointManager = GetComponent<PointManager>();
    }

    void Update()
    {

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
}
