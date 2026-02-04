using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float duracion = GetClipLengh("Explotion");
        animator.SetTrigger("exp");
        Destroy(gameObject, duracion);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigg");
        if (collision.IsTouchingLayers(LayerMask.GetMask("Pared")))
        {
            if (PlayerPrefs.GetInt("Direccion") == 1)
            {
                PlayerPrefs.SetInt("Direccion", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Direccion", 1);
            }
        }
    }

    public float GetClipLengh(String name)
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