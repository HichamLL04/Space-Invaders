using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Animator animator;
    bool isExplosioning = false;
    
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


    public float GetClipLengh(String name)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in clips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }
        return 0f;
    }
}