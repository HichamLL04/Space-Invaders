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
            float duracion = GetClipLengh("Explotion");
            animator.SetTrigger("exp");
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

    public float GetClipLengh(string name)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name)
                return clip.length;
        }
        return 0f;
    }

    public void Disparar()
    {
        GameObject ataque = Instantiate(prefabAtack, transform.position, Quaternion.identity);
        ataque.transform.parent = transform;
        ataque.transform.SetParent(null);
        gameManager.PlayOnce(attackClip);
    }
}