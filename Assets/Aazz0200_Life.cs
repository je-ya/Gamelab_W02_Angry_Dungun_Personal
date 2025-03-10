using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Aazz0200_Life : MonoBehaviour
{
    public float now = 100;
    public float max = 100;

    public Team team;
    public TextMesh ui;

    Slider _playerHp; // 플레이어 체력바 
    GameObject _iceBossRelic;// 죽었을때 생성해줄 Relic 참조형 GameObject 변수 
    GameObject _redBossRelic; // 예린 보스 유물
    GameObject _yellowBossRelic; // 현우 보스 유물 
    public int _itemID; // 부위파괴 했을때 넘겨줄 아이템에 대한 정보 

    public UnityEngine.Events.UnityEvent OnDeath;
    public UnityEngine.Events.UnityEvent OnHit;

    public Transform _relicTrans;

    public SpriteRenderer spriteRenderer;
    float blinkInterval = 0.1f; // 깜빡이는 간격 (초 단위)
    float blinkDuration = 0.5f;

    private void Start()
    {
        ui = GetComponentInChildren<TextMesh>();

        _iceBossRelic = (GameObject)Resources.Load("YSH/Ice_Boss_Relic");
        _redBossRelic = (GameObject)Resources.Load("YSH/Red_Boss_Relic");
        _yellowBossRelic = (GameObject)Resources.Load("YSH/Yellow_Boss_Relic");

        //Slider 참조가 있어야 하고 
        if (team == Team.Plyaer)
        {
            _playerHp = GameObject.FindAnyObjectByType<UI_PlayerHp>().GetComponent<Slider>();
            //_playerHp.maxValue = max;
            _playerHp.value = now / max;
        }
    }
    private void Update()
    {
        if (ui)
            ui.text = now.ToString();

        if (team == Team.Plyaer && now >= 100)
            now = 100;
    }
    public void Hit(float daage)
    {
        now -= daage;
        if (team == Team.Plyaer)
        {
            _playerHp.value = now / max;
            StartCoroutine(Blink());
        }

        if (now <= 0)
            OnDeath.Invoke();
    }
    public void Dest()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 보스 큰 총알에서 사용할 이벤트 
    /// </summary>
    public void BossBigBulletEvent()
    {
        BossBigBulletDestroyManager.Instance.BossBigBulletDestroy();
        ItemGetEventManager.Instance.GetItem(_itemID);
    }
    // 보스가 Relic을 Drop하게 되는 과정 
    /// <summary>
    /// Drop 할때는 보스가 죽은걸로 판정이기 때문에 전체 삭제 
    /// </summary>
    public void IceBossDropRelic()
    {
        Instantiate(_iceBossRelic, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }

    public void IceBossHandDestroy()
    {
        // 손을 파괴 했을때 아이템이 들어오도록 설정 
        ItemGetEventManager.Instance.GetItem(_itemID);
        Destroy(gameObject);
    }

    public void YelloBossDropRelic()
    {
        Instantiate(_yellowBossRelic, _relicTrans.transform.position, Quaternion.identity);
        // spawner 없애면 됨
        Destroy(transform.parent.gameObject);
    }

    public void YelloBossHandDestroy()
    {
        ItemGetEventManager.Instance.GetItem(_itemID);
        Destroy(gameObject);
    }

    public void RedBossDropRelic()
    {
        /////* Boss 죽은자리에 두는 경우 transform.position.

        Instantiate(_redBossRelic, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }

    public void RedBossHandDestroy()
    {
        ItemGetEventManager.Instance.GetItem(_itemID);
        Destroy(gameObject);
    }

    private IEnumerator Blink()
    {
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            // 스프라이트 렌더러를 끔
            spriteRenderer.enabled = false;

            // 지정된 간격만큼 대기
            yield return new WaitForSeconds(blinkInterval);

            // 스프라이트 렌더러를 켬
            spriteRenderer.enabled = true;

            // 다시 지정된 간격만큼 대기
            yield return new WaitForSeconds(blinkInterval);

            // 경과 시간 업데이트
            elapsedTime += blinkInterval * 2;
        }

        // 블링크 효과가 끝난 후 스프라이트 렌더러를 켜진 상태로 유지
        spriteRenderer.enabled = true;
    }
}
