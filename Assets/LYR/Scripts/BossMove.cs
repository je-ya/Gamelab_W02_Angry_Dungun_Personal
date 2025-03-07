using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    private Vector3 targetPosition; // 목적지 위치
    private bool isMoving = false; // 이동 중인지 여부
    private bool isWaiting = false; // 대기 중인지 여부

    void Start()
    {
        // 초기 목적지 설정
        SetNewTargetPosition();

        // 10초 후에 출발
        StartCoroutine(StartMovingAfterDelay(10f));
    }

    void Update()
    {
        if (isMoving && !isWaiting)
        {
            // 목적지로 이동
            MoveToTarget();
        }
    }

    void SetNewTargetPosition()
    {

        float randomX = Random.Range(-130,-70);
        float randomY = Random.Range(-30,30);


        targetPosition = new Vector3(randomX, randomY, 0);
    }

    void MoveToTarget()
    {
        // 목적지로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목적지에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 도착 후 2초 대기
            StartCoroutine(WaitAtDestination(2f));
        }
    }

    IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true; // 10초 후 이동 시작
    }

    IEnumerator WaitAtDestination(float waitTime)
    {
        isWaiting = true; // 대기 상태 시작
        yield return new WaitForSeconds(waitTime); // 2초 대기

        // 새로운 목적지 설정
        SetNewTargetPosition();
        isWaiting = false; // 대기 상태 종료
    }
}
