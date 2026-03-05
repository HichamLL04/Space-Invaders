using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class OvniManager : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody2D myRb;
    Animator animator;
    GameManager gameManager;
    private Vector2 velocidadGuardada;
    private bool moviendo = false;


    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
        Movimiento();
    }


    void Update()
    {
        if (!moviendo)
        {
            StartCoroutine(Movimiento());
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fondo"))
        {
            if (Time.timeScale == 0f)
                return;

            Debug.Log("OnTriggerExit2D");
            GameManager.isCounting = false;
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
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


    IEnumerator Movimiento()
    {
        myRb.linearVelocity = new Vector2(speed, 0);
        moviendo = true;
        yield return null;
    }

    public void PausarMovimiento()
    {
        StopAllCoroutines();
        moviendo = false;
    }
}