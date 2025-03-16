using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 슬롯이 비어있는지 확인 후에 넘겨줄것인지 말것인지 판단 하려함.
/// </summary>
public class Inventory_Slot : MonoBehaviour
{
    public List<Inventory_IsEmpty> _inventorySlots;
    public List<ItemCount> _inventorySlotItemCountText; // 아이템 슬롯 아이템 갯수 카운트

    private void Start()
    {
        // sort를 해서 했더니 3번부터 들어가네 
        // _inventorySlots = new List<Inventory_IsEmpty>(FindObjectsByType<Inventory_IsEmpty>(FindObjectsSortMode.InstanceID));

        // Action 연결 해주면 될듯?
        ItemGetEventManager.Instance.OnItemGetEvent += InventorySlot_ImageChange;
    }

    /// <summary>
    /// 인벤토리 슬롯에 들어가는 애들을 이미지 바꾸는 방식으로 설정 하는 거 
    /// </summary>
    /// <param name="ItemID"></param>
    void InventorySlot_ImageChange(int ItemID)
    {
        //for (int i = 0; i < _inventorySlots.Count; i++)
        //{
        //    if (!_inventorySlots[i].IsEmpty && _inventorySlots[i].ItemID == ItemID)
        //    {
        //        _inventorySlotItemCountText[i].ItemCounts++;
        //        return;
        //    }
        //    else if (_inventorySlots[i].IsEmpty)
        //    {
        //        _inventorySlots[i].GetComponent<Image>().sprite = ItemDataBase.ItemData[ItemID]._itemImage;
        //        _inventorySlots[i].IsEmpty = false;
        //        _inventorySlots[i].ItemID = ItemID;
        //        _inventorySlotItemCountText[i].ItemCounts++;
        //        return;
        //    }
        //
        //}

        // 1. 먼저 같은 ItemID가 있는지 확인하고, 있으면 개수만 증가
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (!_inventorySlots[i].IsEmpty && _inventorySlots[i].ItemID == ItemID)
            {
                _inventorySlotItemCountText[i].ItemCounts++;
                return; // 같은 아이템을 찾았으니 종료
            }
        }

        // 2. 같은 ItemID가 없으면 빈 슬롯에 추가
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (_inventorySlots[i].IsEmpty)
            {
                _inventorySlots[i].GetComponent<Image>().sprite = ItemDataBase.ItemData[ItemID]._itemImage;
                _inventorySlots[i].IsEmpty = false;
                _inventorySlots[i].ItemID = ItemID;
                _inventorySlotItemCountText[i].ItemCounts = 1; // 새로 추가이므로 1로 초기화
                return; // 빈 슬롯에 추가했으니 종료
            }
        }
    }

}
