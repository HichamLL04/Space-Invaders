using UnityEditor.Callbacks;
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
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Pared") || collision.gameObject.layer == LayerMask.NameToLayer("Block"))
        //{
            Destroy(gameObject);
        //}
    }
}
