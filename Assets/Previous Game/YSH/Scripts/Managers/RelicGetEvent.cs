using System;
using UnityEngine;

/// <summary>
/// 유물 주웠을때 발생 하는 이벤트
/// </summary>
public class RelicGetEvent : MonoBehaviour
{
    public static RelicGetEvent Instance => _instance;
    static RelicGetEvent _instance;

    public Action<int> OnRelicGetEvent;

    private void Awake()
    {
        _instance = this;
    }

    public void RelicGet(int relicID)
    {
        OnRelicGetEvent?.Invoke(relicID);
    }


}
