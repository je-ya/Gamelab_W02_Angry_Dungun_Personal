using System.Collections;
using UnityEngine;

/// <summary>
/// 보스 총알중에 조금더 일찍 파괴되어야 할거 같은 플레이어 뒤에 쯤에서 터지는 총알을 만들기 위한 스크립트 
/// 그냥 터지는거 
/// </summary>
public class BossBigBulletDestroy : MonoBehaviour
{
    GameObject _bossBabyBullet; // 생성될 작은 총알 프리팹 
    float _bossBulletDestroyTime = 3f;
    [SerializeField] GameObject[] _targetPos; // Babybullet 생성될 위치에 대한 참조 
    //Todo: 추후 SerializeField 때야됨

    void Start()
    {
        Init();
    }
    /// <summary>
    /// 초기화 + 코루틴 
    /// </summary>
    void Init()
    {
        BossBigBulletDestroyManager.Instance.OnBigBulletDestroyEvent += InstantiateBabyBullet;

        _bossBabyBullet = (GameObject)Resources.Load("YSH/Boss_BigBullet_Baby");

        if (gameObject != null)
            StartCoroutine(DestroyBossBigBullet());
    }

    /// <summary>
    /// 보스 총알을 어떻게 할지에 대한 스크립트 
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyBossBigBullet()
    {
        yield return new WaitForSeconds(_bossBulletDestroyTime);

        if (gameObject != null)
            InstantiateBabyBullet();
    }

    /// <summary>
    /// BabyBullet 생성 
    /// </summary>
    /// <param name="babyBullet"></param>
    void InstantiateBabyBullet()
    {
        for (int i = 0; i < _targetPos.Length; i++)
        {
            if (_targetPos[i] != null)
            {
                Vector2 shootDir = (Vector2)(_targetPos[i].transform.position - transform.position).normalized;

                GameObject go = Instantiate(_bossBabyBullet, (Vector2)transform.position + shootDir, Quaternion.identity);

                go.transform.parent = GameObject.FindAnyObjectByType<SpawnManager>().gameObject.transform;

                float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
                go.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
        }

        if (gameObject == null)
            return;
        BossBigBulletDestroyManager.Instance.OnBigBulletDestroyEvent -= InstantiateBabyBullet;
        Destroy(this.gameObject);
    }
}
