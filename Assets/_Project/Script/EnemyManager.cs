using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Animator animator;
    private static float lastDirectionChangeTime = 0f;
    public static int direccion = 1;
    public static bool cambioDireccion = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            float duracion = GetClipLengh("Explotion");
            animator.SetTrigger("exp");
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
}