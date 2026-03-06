using System.Collections;
using NUnit.Framework.Constraints;
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
    }


    void Update()
    {
        if (GameManager.isPaused)
        {
            myRb.linearVelocity = Vector2.zero;
            return;
        }

        myRb.linearVelocity = new Vector2(speed, 0);
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.isPaused)
            return;

        if (collision.CompareTag("Fondo"))
        {
            Debug.Log("OnTriggerExit2D");
            GameManager.isCounting = false;
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.isPaused)
            return;

        if (collision.gameObject.CompareTag("Attack"))
        {
            Debug.Log("OnCollisionEnter2D");
            float duracion = gameManager.GetClipLengh("Explotion", animator);
            gameManager.IncreaseScore(gameObject.tag);
            animator.SetTrigger("hit");
            GameManager.isCounting = false;
            Destroy(gameObject, duracion);
        }
    }
}