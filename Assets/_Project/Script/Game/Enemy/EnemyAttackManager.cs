using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Rigidbody2D myRb;
    GameManager gameManager;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.linearVelocityY = -speed;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.RestarVida();
        }
        Destroy(gameObject);

    }
}
