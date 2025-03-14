using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public float forwardSpeed = 5f;  // D키 (오른쪽) 이동 속도
    public float backwardSpeed = 3f;    //A키 (왼쪽) 이동속도

    [SerializeField]
    bool inBattle = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        inBattle = StateManager.Instance.CheckBattleActive();
        if (inBattle)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (!inBattle)
        {
            //rb.linearVelocity = movementInput.normalized * moveSpeed;
            float speed = movementInput.x > 0 ? forwardSpeed : backwardSpeed;
            rb.linearVelocity = new Vector2(movementInput.x, 0f).normalized * speed;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}