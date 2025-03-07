using UnityEngine;

public class SlowField : MonoBehaviour
{
    public float slowSpeed = 2f; // 장판에서 적용할 느린 속도
    public bool slowOn = false;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인 (태그 사용)
        if (other.CompareTag("Player"))
        {
            slowOn = true;
            Aazz0200_Player player = other.GetComponent<Aazz0200_Player>();
            if (player != null)
            {

                Debug.Log("플레이어 속도가 느려졌습니다!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Aazz0200_Player player = other.GetComponent<Aazz0200_Player>();
            if (player != null)
            {

                Debug.Log("플레이어 속도가 정상으로 돌아왔습니다!");
            }
        }
    }

}