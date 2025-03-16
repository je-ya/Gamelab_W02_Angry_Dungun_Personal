using UnityEngine;

/// <summary>
/// Boss 기본 총알 시간 지나면 파괴 코드 
/// </summary>
public class DestroyBossBullet : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }
}
