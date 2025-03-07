using UnityEngine;

public class Alter : MonoBehaviour
{
    public bool AlterActive => _alterActive;
    int _relicID; // ���� ������ ��� �ִ� RelicID

    bool _alterActive = false; // �ش�  ������ ������ �ִ���

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
            _alterActive = true;
        }
    }
}
