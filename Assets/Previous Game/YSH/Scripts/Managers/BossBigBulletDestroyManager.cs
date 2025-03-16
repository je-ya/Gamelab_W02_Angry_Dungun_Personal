using System;
using UnityEngine;

/// <summary>
/// 보스총알 파괴 되었을 시점에 적용시켜줄 Action 실행 
/// </summary>
public class BossBigBulletDestroyManager : MonoBehaviour
{
    public static BossBigBulletDestroyManager Instance => _instance;
    static BossBigBulletDestroyManager _instance;

    public Action OnBigBulletDestroyEvent; // 보스 큰 총알 파괴 이벤트 연결부

    private void Awake()
    {
        _instance = this;
    }
    // 이게 함수를 실행하는 부분 파괴되면 실행할꺼 
    public void BossBigBulletDestroy()
    {
        OnBigBulletDestroyEvent?.Invoke();
    }
}
