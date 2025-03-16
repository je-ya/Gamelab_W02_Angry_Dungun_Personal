using UnityEngine;

public class KHW_BossHandMove : MonoBehaviour
{
    [SerializeField] float moveCoolDown = 5f;      // 이동 간격
    [SerializeField] float elapsedTime = 0f;       // 경과 시간
    [SerializeField] bool isMoved = false;         // 이동 중 여부
    [SerializeField] GameObject Hand;              // 손 오브젝트
    [SerializeField] float moveTime = 3f;
    [SerializeField] Transform originTransform;
    [SerializeField] Transform moveTransform;      // 목표 위치 (월드 좌표)

    float moveTimer = 0f;                          // 이동 타이머
    bool moveToTarget = false;                     // 이동 방향
    Vector3 startWorldPos;                         // 월드 시작 위치

    private void Start()
    {
        // Hand의 초기 월드 위치 저장
        startWorldPos = Hand.transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // 쿨다운 체크
        if (elapsedTime > moveCoolDown && !isMoved)
        {
            MoveHand();
        }

        // 이동 중일 때 부드럽게 처리
        if (isMoved)
        {
            moveTimer += Time.deltaTime;
            float t = moveTimer / moveTime;

            if (t <= 1f)
            {
                Vector3 fromPos = moveToTarget ? startWorldPos : moveTransform.position;
                Vector3 toPos = moveToTarget ? moveTransform.position : startWorldPos;
                Hand.transform.position = Vector3.Lerp(fromPos, toPos, t);
            }
            else
            {
                // 이동 완료
                isMoved = false;
                moveTimer = 0f;
                elapsedTime = 0f;
                moveToTarget = !moveToTarget; // 방향 전환
            }
        }
    }

    void MoveHand()
    {
        if (!isMoved)
        {
            isMoved = true;
            moveTimer = 0f;
        }
    }
}