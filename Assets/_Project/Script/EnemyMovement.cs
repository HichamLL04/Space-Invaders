using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager[] enemies;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float velocidad = 0.6875f;
    [SerializeField] float caida = 1;
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
        if (!EnemyManager.cambioDireccion)
        {
            moveBox.transform.Translate(Vector3.right * velocidad * direccion);
        }
        else
        {
            moveBox.transform.Translate(Vector3.down * caida);
            StartCoroutine(Pause());
            moveBox.transform.Translate(Vector3.right * velocidad * direccion);
            EnemyManager.cambioDireccion = false;
        }

        yield return new WaitForSeconds(cooldown);
        
        moviendo = false;
    }

    EnemyManager[] GetEnemies()
    {
        return FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);
    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(2f);
    }
}