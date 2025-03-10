using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Aazz0200_Life : MonoBehaviour
{
    public float now = 100;
    public float max = 100;

    public Team team;
    public TextMesh ui;

    Slider _playerHp; // �÷��̾� ü�¹� 
    GameObject _iceBossRelic;// �׾����� �������� Relic ������ GameObject ���� 
    GameObject _redBossRelic; // ���� ���� ����
    GameObject _yellowBossRelic; // ���� ���� ���� 
    public int _itemID; // �����ı� ������ �Ѱ��� �����ۿ� ���� ���� 

    public UnityEngine.Events.UnityEvent OnDeath;
    public UnityEngine.Events.UnityEvent OnHit;

    public Transform _relicTrans;

    public SpriteRenderer spriteRenderer;
    float blinkInterval = 0.1f; // �����̴� ���� (�� ����)
    float blinkDuration = 0.5f;

    private void Start()
    {
        ui = GetComponentInChildren<TextMesh>();

        _iceBossRelic = (GameObject)Resources.Load("YSH/Ice_Boss_Relic");
        _redBossRelic = (GameObject)Resources.Load("YSH/Red_Boss_Relic");
        _yellowBossRelic = (GameObject)Resources.Load("YSH/Yellow_Boss_Relic");

        //Slider ������ �־�� �ϰ� 
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
    /// ���� ū �Ѿ˿��� ����� �̺�Ʈ 
    /// </summary>
    public void BossBigBulletEvent()
    {
        BossBigBulletDestroyManager.Instance.BossBigBulletDestroy();
        ItemGetEventManager.Instance.GetItem(_itemID);
    }
    // ������ Relic�� Drop�ϰ� �Ǵ� ���� 
    /// <summary>
    /// Drop �Ҷ��� ������ �����ɷ� �����̱� ������ ��ü ���� 
    /// </summary>
    public void IceBossDropRelic()
    {
        Instantiate(_iceBossRelic, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }

    public void IceBossHandDestroy()
    {
        // ���� �ı� ������ �������� �������� ���� 
        ItemGetEventManager.Instance.GetItem(_itemID);
        Destroy(gameObject);
    }

    public void YelloBossDropRelic()
    {
        Instantiate(_yellowBossRelic, _relicTrans.transform.position, Quaternion.identity);
        // spawner ���ָ� ��
        Destroy(transform.parent.gameObject);
    }

    public void YelloBossHandDestroy()
    {
        ItemGetEventManager.Instance.GetItem(_itemID);
        Destroy(gameObject);
    }

    public void RedBossDropRelic()
    {
        /////* Boss �����ڸ��� �δ� ��� transform.position.

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
            // ��������Ʈ �������� ��
            spriteRenderer.enabled = false;

            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkInterval);

            // ��������Ʈ �������� ��
            spriteRenderer.enabled = true;

            // �ٽ� ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkInterval);

            // ��� �ð� ������Ʈ
            elapsedTime += blinkInterval * 2;
        }

        // ��ũ ȿ���� ���� �� ��������Ʈ �������� ���� ���·� ����
        spriteRenderer.enabled = true;
    }
}
