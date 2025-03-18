using UnityEngine;
using UnityEngine.InputSystem;

//캐릭터의 A,D(좌우) 움직임
public class PlayerMove : MonoBehaviour
{
    //rb 사용
    //    private Rigidbody2D rb;
    //    private Vector2 movementInput;
    //    public float forwardSpeed = 5f;  // D키 (오른쪽) 이동 속도
    //    public float backwardSpeed = 3f;    //A키 (왼쪽) 이동속도

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
    public float forwardSpeed = 5f;  // D키 (오른쪽) 이동 속도
    public float backwardSpeed = 3f; // A키 (왼쪽) 이동속도

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

    // Input System과 연결될 메서드
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}

