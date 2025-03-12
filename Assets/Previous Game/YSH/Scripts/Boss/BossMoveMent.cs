using UnityEngine;

/// <summary>
/// 보스 회전 속도 관련 스크립트 
/// </summary>
public class BossMoveMent : MonoBehaviour
{
    float _bossRotateSpeed = 100f; // Boss 회전 속도 

    void Update()
    {
        transform.Rotate(0, 0, 1 * _bossRotateSpeed * Time.deltaTime); //속도를 늘리면 될듯?
    }
}
