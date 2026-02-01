using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D myRb;
    Vector2 moveInput;
    
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, 0f);
        myRb.linearVelocity = playerVelocity;
    }

    public void OnAttack()
    {
        
    }
}