using TMPro;
using UnityEngine;

/// <summary>
/// 현재 해당 스크립트 사용 안함 
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
