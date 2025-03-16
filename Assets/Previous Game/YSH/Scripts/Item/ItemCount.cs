using TMPro;
using UnityEngine;

public class ItemCount : MonoBehaviour
{
    TMP_Text _itemCountText;

    public int ItemCounts
    {
        get
        {
            return _itemCount;
        }
        set
        {
            _itemCount = value;
            _itemCountText.text = $"{_itemCount}";
        }
    }

    int _itemCount = 0;

    private void Start()
    {
        _itemCountText = GetComponent<TMP_Text>();
    }


}
