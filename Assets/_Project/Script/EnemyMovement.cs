using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager[] enemies;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float velocidad = 0.6875f;
    private bool moviendo = false;
    GameObject moveBox;

    void Start()
    {
        enemies = GetEnemies();
        moveBox = gameObject;
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
        
        int direccion = EnemyManager.direccion;
        /*
        foreach (EnemyManager enemy in enemies)
        {
            enemy.transform.Translate(Vector3.right * velocidad * direccion);
        }
        */
        moveBox.transform.Translate(Vector3.right * velocidad * direccion);
        
        yield return new WaitForSeconds(cooldown);
        
        moviendo = false;
    }

    EnemyManager[] GetEnemies()
    {
        return FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
    }
}