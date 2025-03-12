using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static Dictionary<int, Item_Data> ItemData => _itemDataBase; // 어디서든지 가지고 올수 있도록 만들고 
    static Dictionary<int, Item_Data> _itemDataBase = new Dictionary<int, Item_Data>(); // 추가 함 


    private void Awake()
    {
        AddItemToDataBase(11, "YSH/ItemData/SpeedUpItem");
        AddItemToDataBase(12, "YSH/ItemData/ShootSpeedUpItem");
        AddItemToDataBase(13, "YSH/ItemData/Potion_Item");
    }


    private void AddItemToDataBase(int key, string resourcePath)
    {
        if (!_itemDataBase.ContainsKey(key))
        {
            _itemDataBase.Add(key, (Item_Data)Resources.Load(resourcePath));
        }
        else
        {
            return;
        }
    }
}
