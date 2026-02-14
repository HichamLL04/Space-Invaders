using UnityEngine;

public class OvniManager : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody2D myRb;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
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

    void Movimiento()
    {
        myRb.linearVelocity = new Vector2(speed, 0);
    }
}
