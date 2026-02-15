using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject prefabAtack;
    [SerializeField] float tiempoAtaque = 1;
    [SerializeField] AudioClip attackClip;
    float cooldownRestante;
    bool canAttack = false;
    Rigidbody2D myRb;
    Vector2 moveInput;
    GameManager gameManager;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        cooldownRestante = tiempoAtaque;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (cooldownRestante > 0)
        {
            cooldownRestante -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, 0f);
        myRb.linearVelocity = playerVelocity;
    }

    public void OnAttack()
    {
        if (canAttack)
        {
            GameObject ataque = Instantiate(prefabAtack, transform.position, Quaternion.identity);
            ataque.transform.parent = transform;
            gameManager.PlayOnce(attackClip);
            canAttack = false;
            cooldownRestante = tiempoAtaque;
        }
    }

    public void Escape()
    {
        gameManager.TogglePause();
    }
}