using System;
using UnityEngine;

public class InvenItemCountEventManager : MonoBehaviour
{
    public static InvenItemCountEventManager Instance => _instance;
    static InvenItemCountEventManager _instance;

    public Action<int> OnItemCountChange; // 아이템 갯수를 바꿀때 사용할 액션  

    private void Awake()
    {
        _instance = this;
    }

    public void ItemCountChange(int itemCounts)
    {
        OnItemCountChange?.Invoke(itemCounts);
    }

}
