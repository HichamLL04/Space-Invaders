using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float cooldown = 1f;
    [SerializeField] float velocidad = 1;
    [SerializeField] float caida = 1;
    [SerializeField] float probabilidadAtaque = 0.10f;
    private bool moviendo = false;
    GameObject moveBox;
    GameManager gameManager;
    Vector3 posicionInicial;
    float velOriginal = 0;


    void Start()
    {
        moveBox = gameObject;
        gameManager = FindFirstObjectByType<GameManager>();
        posicionInicial = gameObject.transform.position;
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
        foreach (EnemyManager enemy in gameManager.GetEnemies())
        {
            if (UnityEngine.Random.value < probabilidadAtaque)
            {
                enemy.Disparar();
                return;
            }
        }
    }


    public void SetLocation()
    {
        gameObject.transform.position = posicionInicial;
    }


    public void SetVelocidad(float nuevaVelocidad)
    {
        velocidad += nuevaVelocidad;
    }


    public void ContVelocidad(bool activar)
    {
        if (!activar)
        {
            velOriginal = velocidad;
            velocidad = 0;
        }
        else
        {
            velocidad = velOriginal;
        }
    }


    IEnumerator Pause(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
    }

}