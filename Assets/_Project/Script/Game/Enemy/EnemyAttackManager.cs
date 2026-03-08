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


    void Update()
    {
        if (GameManager.isPaused)
        {
            myRb.linearVelocity = Vector2.zero;
        }
        else
        {
            myRb.linearVelocityY = -speed;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.isPaused)
            return;

        if (!trigger)
        {
            Delete();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.isPaused)
            return;

        if (collision.gameObject.CompareTag("Player") && !trigger)
        {
            trigger = true;

            foreach (Collider2D col in GetComponents<Collider2D>())
            {
                col.enabled = false;
            }

            myRb.bodyType = RigidbodyType2D.Kinematic;
            myRb.linearVelocity = Vector2.zero;
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