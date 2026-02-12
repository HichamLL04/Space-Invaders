using UnityEngine;

public class OvniManager : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody2D myRb;
    int exit = 0;
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
        if (collision.CompareTag("Pared"))
        {
            exit += 1;
            Debug.Log("Sale");
            if (exit == 2)
            {
                Destroy(gameObject);
            }
        }
    }

    void Movimiento()
    {
        myRb.linearVelocity = new Vector2(speed, 0);
    }
}
