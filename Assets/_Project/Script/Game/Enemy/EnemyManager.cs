using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject prefabAtack;
    [SerializeField] AudioClip attackClip;

    GameManager gameManager;
    Animator animator;
    private static float lastDirectionChangeTime = 0f;
    public static int direccion = 1;
    public static bool cambioDireccion = false;

    EnemyMovement enemyMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        gameManager = FindFirstObjectByType<GameManager>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            float duracion = gameManager.GetClipLengh("Explotion", animator);
            animator.SetTrigger("exp");
            gameManager.IncreaseScore(gameObject.tag);
            gameManager.SetEnemy();
            Destroy(gameObject, duracion);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            if (Time.time - lastDirectionChangeTime > 2f)
            {
                lastDirectionChangeTime = Time.time;
                direccion *= -1;
                cambioDireccion = true;
            }
        }
    }

    public void Disparar()
    {
        GameObject ataque = Instantiate(prefabAtack, transform.position, Quaternion.identity);
        ataque.transform.parent = transform;
        ataque.transform.SetParent(null);
        gameManager.PlayOnce(attackClip);
    }
}