using UnityEngine;

/// <summary>
/// 보스 몸에서 나가는 총알이 어떻게 나갈지에 대한 생각으로 만드는 코드 
/// </summary>
public class BossBulletAttack : MonoBehaviour
{
    GameObject _bossBullet; // Boss 총알에 대한 프리팹
    GameObject _player;

    float _startInvoke = 0f;
    float _repeatInvoke = 0.5f;


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
        _bossBullet = Resources.Load<GameObject>("YSH/IceBoss_Bullet");
        _player = GameObject.FindAnyObjectByType<Aazz0200_Player>().gameObject;
    }
    /// <summary>
    /// Action 혹은 Invoke 실행 정의 함수 
    /// </summary>
    void ActionInvoke()
    {
        InvokeRepeating("InstanceBullet", _startInvoke, _repeatInvoke);
    }
    /// <summary>
    /// 총알 생성 함수 
    /// </summary>
    void InstanceBullet()
    {
        //플레이어가 사라지면서 참조를 못받아서 오류가 뜸
        if (_player == null)
            return;

        Vector2 shootDir = (Vector2)(_player.transform.position - transform.position).normalized;
        GameObject go = Instantiate(_bossBullet, (Vector2)transform.position + shootDir, Quaternion.identity);

        go.transform.parent = GameObject.FindAnyObjectByType<SpawnManager>().gameObject.transform;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
