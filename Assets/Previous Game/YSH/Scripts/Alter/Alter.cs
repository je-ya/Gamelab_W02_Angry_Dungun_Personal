using UnityEngine;

public class Alter : MonoBehaviour
{
    public bool AlterActive => _alterActive;
    int _relicID; // 현재 제단이 들고 있는 RelicID

    bool _alterActive = false; // 해당  유물을 가지고 있는지

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
