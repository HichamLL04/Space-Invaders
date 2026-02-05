using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager[] enemies;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float velocidad = 0.6875f;
    [SerializeField] float caida = 1;
    [SerializeField] float probabilidadAtaque = 0.02f;
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

        if (!EnemyManager.cambioDireccion)
        {
            moveBox.transform.Translate(Vector3.right * velocidad * direccion);
            EnemyAttack();
        }
        else
        {
            moveBox.transform.Translate(Vector3.down * caida);
            StartCoroutine(Pause(2f));
            moveBox.transform.Translate(Vector3.right * velocidad * direccion);
            EnemyManager.cambioDireccion = false;
        }
        yield return new WaitForSecondsRealtime(cooldown);
        moviendo = false;
    }

    void EnemyAttack()
    {
        foreach (EnemyManager enemy in enemies)
    {
        if (UnityEngine.Random.value < probabilidadAtaque)
        {
            enemy.Disparar();
        }
    }
    }

    EnemyManager[] GetEnemies()
    {
        return FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
    }

    public void SetEnemy()
    {
        enemies = GetEnemies();
    }

    IEnumerator Pause(float cooldown)
    {
        Debug.Log("cool");
        yield return new WaitForSecondsRealtime(cooldown);
    }
}