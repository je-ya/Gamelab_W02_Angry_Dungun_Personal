using System.Collections.Generic;
using UnityEngine;

public class Worm_circle : MonoBehaviour
{
    public Transform target; // 따라갈 대상
    public float fSpeed = 0.1f; // 부드러운 이동 속도
    public int delay = 10; // 따라가는 딜레이 (프레임 단위)

    private Queue<Vector3> positions = new Queue<Vector3>(); // 위치 저장 큐

    public void UpdatePosition()
    {
        // 타겟의 현재 위치를 큐에 추가
        positions.Enqueue(target.position);

        // 딜레이보다 큐가 커지면 이전 위치를 따라가도록 설정
        if (positions.Count > delay)
        {
            Vector3 targetPosition = positions.Dequeue();

            // 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, fSpeed);
        }
    }
}