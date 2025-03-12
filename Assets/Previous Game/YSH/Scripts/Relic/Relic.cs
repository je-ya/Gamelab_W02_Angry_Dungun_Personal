using UnityEngine;

/// <summary>
/// 플레이어가 들고 있는 Relic에 대한 설명 
/// </summary>
public class Relic : MonoBehaviour
{
    public bool RelicActive => _relicActive;
    int _relicID; // 현재 플레이어가 들고 있는 RelicID

    bool _relicActive = false; // 해당  유물을 가지고 있는지

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
