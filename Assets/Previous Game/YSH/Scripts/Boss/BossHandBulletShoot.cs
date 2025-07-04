using UnityEngine;
/// <summary>
/// 보스 손 앞을 타겟으로 지정하고 해당 방향을 발사함
/// </summary>
public class BossHandBulletShoot : MonoBehaviour
{
    [Header("BossBullet")]
    GameObject _bossBullet;

    [Header("Target")]
    GameObject _handTarget; // 보스 손에서 나가는 위치 설정 

    void Start()
    {
        Init();
        InvokeRepeating("BossShoot", 1f, 0.2f);
    }

    void Init()
    {
        _bossBullet = Resources.Load<GameObject>("YSH/IceBoss_Bullet");
        _handTarget = transform.GetChild(0).gameObject;
    }

    void BossShoot()
    {
        Vector2 shootDir = (Vector2)(_handTarget.transform.position - transform.position).normalized;

        GameObject go = Instantiate(_bossBullet, (Vector2)transform.position + shootDir, Quaternion.identity);
        go.transform.parent = GameObject.FindAnyObjectByType<SpawnManager>().gameObject.transform;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
