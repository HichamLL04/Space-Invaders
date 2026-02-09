using UnityEngine;

public class BrickManager : MonoBehaviour
{
    int hit = 1;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            switch (hit)
            {
                case 1:
                    animator.SetTrigger("OneHit");
                    break;
                case 2:
                    animator.SetTrigger("SecondHit");
                    break;
                case 3:
                    animator.SetTrigger("ThirdHit");
                    Destroy(gameObject, 0.1f);
                    break;
                default:
                    Destroy(gameObject, 0.2f);
                    break;
            }
        }
        hit += 1;
    }
}
