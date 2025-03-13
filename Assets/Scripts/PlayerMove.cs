using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public float forwardSpeed = 5f;  // DŰ (������) �̵� �ӵ�
    public float backwardSpeed = 3f;    //AŰ (����) �̵��ӵ�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //rb.linearVelocity = movementInput.normalized * moveSpeed;
        float speed = movementInput.x > 0 ? forwardSpeed : backwardSpeed;
        rb.linearVelocity = new Vector2(movementInput.x, 0f).normalized * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}