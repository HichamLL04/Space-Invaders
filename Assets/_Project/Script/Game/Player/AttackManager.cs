using UnityEngine;

public class AttackManager : MonoBehaviour
{
    float speed = 10f;
    Rigidbody2D myRb;


    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.linearVelocityY = speed;
    }


    void Update()
    {

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}