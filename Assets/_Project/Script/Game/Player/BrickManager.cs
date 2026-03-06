using UnityEngine;

public class BrickManager : MonoBehaviour
{
    int hit = 0;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            hit += 1;
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
                    gameObject.SetActive(false);
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }


    public void EnableBrick()
    {
        hit = 0;
        ResetTrigger();
        gameObject.SetActive(true);
        animator.SetTrigger("NextWave");
    }


    public void ResetTrigger()
    {
        animator.ResetTrigger("OneHit");
        animator.ResetTrigger("SecondHit");
        animator.ResetTrigger("ThirdHit");
        animator.ResetTrigger("NextWave");
    }
}
