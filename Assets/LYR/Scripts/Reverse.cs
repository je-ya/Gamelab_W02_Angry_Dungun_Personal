using UnityEngine;

public class Reverse : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        // 오브젝트가 비활성화되어 있는지 확인
        if (!targetObject.activeSelf)
        {
            Debug.Log("대상 오브젝트가 비활성화되어 있습니다.");
        }

        // Collider2D 컴포넌트 가져오기
        Collider2D collider = targetObject.GetComponent<Collider2D>();

        if (collider != null)
        {
            // 월드 좌표 기준 Bounds 가져오기
            Bounds bounds = collider.bounds;

            // Min (왼쪽 아래)과 Max (오른쪽 위) 좌표
            Vector2 min = bounds.min;
            Vector2 max = bounds.max;

            // 결과 출력
            Debug.Log($"Min X: {min.x}, Min Y: {min.y}");
            Debug.Log($"Max X: {max.x}, Max Y: {max.y}");
        }
        else
        {
            Debug.Log("Collider2D가 없습니다.");
        }
    }
}