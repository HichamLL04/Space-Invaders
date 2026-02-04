using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager[] enemies;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float velocidad = 0.6875f;
    private bool moviendo = false;

    void Start()
    {
        enemies = GetEnemies();
    }

    void Update()
    {
        if (!moviendo)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        moviendo = true;
        
        int direccion = PlayerPrefs.GetInt("Direccion", 1);
        foreach (EnemyManager enemy in enemies)
        {
            enemy.transform.Translate(Vector3.right * velocidad * direccion);
        }
        
        yield return new WaitForSeconds(cooldown);
        
        moviendo = false;
    }

    EnemyManager[] GetEnemies()
    {
        return FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
    }
}