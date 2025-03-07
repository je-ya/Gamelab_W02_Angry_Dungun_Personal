using System;
using UnityEngine;

public class InvenItemCountEventManager : MonoBehaviour
{
    public static InvenItemCountEventManager Instance => _instance;
    static InvenItemCountEventManager _instance;

    public Action<int> OnItemCountChange; // ������ ������ �ٲܶ� ����� �׼�  

    private void Awake()
    {
        _instance = this;
    }

    public void ItemCountChange(int itemCounts)
    {
        OnItemCountChange?.Invoke(itemCounts);
    }

}
