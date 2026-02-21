using UnityEngine;

public class OvniManager : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody2D myRb;
    Animator animator;
    GameManager gameManager;


    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
        Movimiento();
    }


    void Update()
    {

    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fondo"))
        {
            GameManager.isCounting = false;
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            float duracion = gameManager.GetClipLengh("Explotion", animator);
            animator.SetTrigger("hit");
            GameManager.isCounting = false;
            Destroy(gameObject, duracion);
        }
    }


    void Movimiento()
    {
        myRb.linearVelocity = new Vector2(speed, 0);
    }
}
