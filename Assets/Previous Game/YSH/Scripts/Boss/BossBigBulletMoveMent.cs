using UnityEngine;

public class BossBigBulletMoveMent : MonoBehaviour
{
    float _bossBigBulletSpeed = 0.1f;
    Rigidbody2D _rigid; // 총알 물리력 구현을 위한 스크립트 

    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        BulletMove();
    }
    /// <summary>
    /// 초기화 함수 
    /// </summary>
    void Init()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// 총알 발사 되었을때 움직임 구현 
    /// </summary>
    void BulletMove()
    {
        _rigid.AddForce(transform.up * _bossBigBulletSpeed, ForceMode2D.Impulse);
    }
}
