using UnityEngine;

/// <summary>
/// 보스 큰 총알 스크립트 팔을 만든다? 
/// 어떤식으로 만들지에 대한 고민 
/// 팔 앞 지점을 기점으로 발사가 되는 방식으로 구현 
/// </summary>
public class BossBigBulletShoot : MonoBehaviour
{
    GameObject _bossBigBullet; // Boss 큰 총알에 대한 프리팹
    GameObject _player; // 플레이어 참조 

    float _startInvoke = 0f; // 시작 시점 
    float _repeatInvoke = 2f; // 반복 시점 


    private void Start()
    {
        Init();
        ActionInvoke();
    }

    /// <summary>
    /// 초기화 함수 
    /// </summary>
    void Init()
    {
        _bossBigBullet = Resources.Load<GameObject>("YSH/IceBoss_BigBullet");
        _player = GameObject.FindAnyObjectByType<Aazz0200_Player>().gameObject;
    }
    /// <summary>
    /// Action 혹은 Invoke 실행 정의 함수 
    /// </summary>
    void ActionInvoke()
    {
        InvokeRepeating("InstanceBigBullet", _startInvoke, _repeatInvoke);
    }
    /// <summary>
    /// 큰 총알 생성 함수 
    /// </summary>
    void InstanceBigBullet()
    {
        if (_player == null)
            return;

        Vector2 shootDir = (Vector2)(_player.transform.position - transform.position).normalized;
        GameObject go = Instantiate(_bossBigBullet, (Vector2)transform.position + shootDir, Quaternion.identity);

        go.transform.parent = GameObject.FindAnyObjectByType<SpawnManager>().gameObject.transform;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
