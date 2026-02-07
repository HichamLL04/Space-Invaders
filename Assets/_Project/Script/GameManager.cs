using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    public static EnemyManager[] enemies;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemies = GetEnemies();
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
}
