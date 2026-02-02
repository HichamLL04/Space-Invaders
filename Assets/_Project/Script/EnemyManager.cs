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
        if (isExplosioning)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
                isExplosioning = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("exp");
        isExplosioning = true;
    }
}