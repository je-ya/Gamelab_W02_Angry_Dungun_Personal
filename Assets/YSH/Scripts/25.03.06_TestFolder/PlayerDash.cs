using UnityEngine;
using System.Collections; // 코루틴 사용을 위해 필요

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashSpeed = 20f;           // 대쉬 속도
    [SerializeField] float dashTime = 0.2f;           // 대쉬 지속 시간
    [SerializeField] float dashCooldown = 1f;         // 대쉬 쿨다운
    [SerializeField] GameObject afterImagePrefab;     // 잔상 프리팹
    [SerializeField] float afterImageLifetime = 0.1f; // 잔상 지속 시간
    [SerializeField] float afterImageInterval = 0.05f;// 잔상 생성 간격

    bool isDashing = false;         // 대쉬 중인지 체크
    bool canDash = true;            // 쿨다운 체크
    Rigidbody2D rb;                 // 물리 기반 이동
    SpriteRenderer playerSprite;    // 플레이어 스프라이트

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 스페이스 바 입력 감지
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    // 대쉬 코루틴
    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        //대쉬 부분은 여기만 손 대면 될 듯?
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        dashDirection =  new Vector2(X, Y).normalized;



        rb.linearVelocity = dashDirection * dashSpeed;
        StartCoroutine(SpawnAfterImages());
        yield return new WaitForSeconds(dashTime);
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // 잔상 생성 코루틴
    IEnumerator SpawnAfterImages()
    {
        while (isDashing)
        {
            // 잔상 생성
            GameObject afterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            afterImage.transform.localScale = transform.localScale; // 방향 일치 (필요 시 수정 가능)
            SpriteRenderer sr = afterImage.GetComponent<SpriteRenderer>();
            sr.sprite = playerSprite.sprite; // 현재 플레이어 스프라이트 복사
            Destroy(afterImage, afterImageLifetime); // 잔상 삭제

            // 간격만큼 대기
            yield return new WaitForSeconds(afterImageInterval);
        }
    }
}