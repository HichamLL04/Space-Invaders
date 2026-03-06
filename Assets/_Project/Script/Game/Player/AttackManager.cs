using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Rigidbody2D myRb;


    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.linearVelocityY = speed;
    }


    void Update()
    {
        if (GameManager.isPaused)
        {
            myRb.linearVelocity = Vector2.zero;
        }
        else
        {
            myRb.linearVelocityY = speed;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.isPaused)
            return;

        Destroy(gameObject);
    }
}