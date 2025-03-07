using TMPro;
using UnityEngine;

/// <summary>
/// ���� �ش� ��ũ��Ʈ ��� ���� 
/// </summary>
public class ItemTextCountChange : MonoBehaviour
{
    TMP_Text _itemCountText;
    int _itemCount;

    void Start()
    {
        _itemCountText = GetComponent<TMP_Text>();
        _itemCount = GetComponent<ItemCount>().ItemCounts;

        InvenItemCountEventManager.Instance.OnItemCountChange += ItemCountTextChange;
    }

    void ItemCountTextChange(int itemCount)
    {
        _itemCount = itemCount;
        _itemCountText.text = $"{_itemCount}";
    }


}
