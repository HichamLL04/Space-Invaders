using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Rigidbody2D myRb;
    GameManager gameManager;
    Animator animator;
    bool trigger = false;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.linearVelocityY = -speed;
        gameManager = FindFirstObjectByType<GameManager>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!trigger)
        {
            Delete();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !trigger)
        {
            gameManager.RestarVida();
            gameManager.Hit();
            Delete();
        }
    }


    void Delete()
    {
        trigger = true;
        float duracion = gameManager.GetClipLengh("Explotion", animator);
        animator.SetTrigger("exp");
        Destroy(gameObject, duracion);
    }
}
