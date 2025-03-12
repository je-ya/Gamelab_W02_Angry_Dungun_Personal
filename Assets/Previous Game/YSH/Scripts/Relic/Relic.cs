using UnityEngine;

/// <summary>
/// �÷��̾ ��� �ִ� Relic�� ���� ���� 
/// </summary>
public class Relic : MonoBehaviour
{
    public bool RelicActive => _relicActive;
    int _relicID; // ���� �÷��̾ ��� �ִ� RelicID

    bool _relicActive = false; // �ش�  ������ ������ �ִ���

    private void Start()
    {
        Init();
    }

    void Init()
    {
        this.gameObject.SetActive(false);
        _relicID = GetComponent<RelicID>()._RelicID;
        RelicGetEvent.Instance.OnRelicGetEvent += GetRelic;
    }

    void GetRelic(int relicID)
    {
        if (_relicID == relicID)
        {
            this.gameObject.SetActive(true);
            _relicActive = true;
        }
    }
}
