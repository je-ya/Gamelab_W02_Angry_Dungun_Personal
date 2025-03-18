using UnityEngine;
using UnityEngine.InputSystem;

//ĳ������ A,D(�¿�) ������
public class PlayerMove : MonoBehaviour
{
    //rb ���
    //    private Rigidbody2D rb;
    //    private Vector2 movementInput;
    //    public float forwardSpeed = 5f;  // DŰ (������) �̵� �ӵ�
    //    public float backwardSpeed = 3f;    //AŰ (����) �̵��ӵ�

    //    [SerializeField]
    //    bool inBattle = false;

    //    void Start()
    //    {
    //        rb = GetComponent<Rigidbody2D>();
    //    }

    //    void FixedUpdate()
    //    {
    //        inBattle = StateManager.Instance.CheckBattleActive();
    //        if (inBattle)
    //        {
    //            rb.linearVelocity = Vector2.zero;
    //        }

    //        if (!inBattle)
    //        {
    //            //rb.linearVelocity = movementInput.normalized * moveSpeed;
    //            float speed = movementInput.x > 0 ? forwardSpeed : backwardSpeed;
    //            rb.linearVelocity = new Vector2(movementInput.x, 0f).normalized * speed;
    //        }
    //    }

    //    public void OnMove(InputAction.CallbackContext context)
    //    {
    //        movementInput = context.ReadValue<Vector2>();
    //    }




    private Vector2 movementInput;
    public float forwardSpeed = 5f;  // DŰ (������) �̵� �ӵ�
    public float backwardSpeed = 3f; // AŰ (����) �̵��ӵ�

    [SerializeField]
    bool inBattle = false;

    void Update()
    {
        inBattle = StateManager.Instance.CheckBattleActive();

        if (!inBattle)
        {
            float speed = movementInput.x > 0 ? forwardSpeed : backwardSpeed;
            Vector2 movement = new Vector2(movementInput.x, 0f).normalized * speed * Time.deltaTime;
            transform.position += (Vector3)movement;
        }
    }

    // Input System�� ����� �޼���
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}

