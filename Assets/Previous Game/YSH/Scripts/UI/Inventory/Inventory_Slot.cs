using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 ������ ����ִ��� Ȯ�� �Ŀ� �Ѱ��ٰ����� �������� �Ǵ� �Ϸ���.
/// </summary>
public class Inventory_Slot : MonoBehaviour
{
    public List<Inventory_IsEmpty> _inventorySlots;
    public List<ItemCount> _inventorySlotItemCountText; // ������ ���� ������ ���� ī��Ʈ

    private void Start()
    {
        // sort�� �ؼ� �ߴ��� 3������ ���� 
        // _inventorySlots = new List<Inventory_IsEmpty>(FindObjectsByType<Inventory_IsEmpty>(FindObjectsSortMode.InstanceID));

        // Action ���� ���ָ� �ɵ�?
        ItemGetEventManager.Instance.OnItemGetEvent += InventorySlot_ImageChange;
    }

    /// <summary>
    /// �κ��丮 ���Կ� ���� �ֵ��� �̹��� �ٲٴ� ������� ���� �ϴ� �� 
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

        // 1. ���� ���� ItemID�� �ִ��� Ȯ���ϰ�, ������ ������ ����
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (!_inventorySlots[i].IsEmpty && _inventorySlots[i].ItemID == ItemID)
            {
                _inventorySlotItemCountText[i].ItemCounts++;
                return; // ���� �������� ã������ ����
            }
        }

        // 2. ���� ItemID�� ������ �� ���Կ� �߰�
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (_inventorySlots[i].IsEmpty)
            {
                _inventorySlots[i].GetComponent<Image>().sprite = ItemDataBase.ItemData[ItemID]._itemImage;
                _inventorySlots[i].IsEmpty = false;
                _inventorySlots[i].ItemID = ItemID;
                _inventorySlotItemCountText[i].ItemCounts = 1; // ���� �߰��̹Ƿ� 1�� �ʱ�ȭ
                return; // �� ���Կ� �߰������� ����
            }
        }
    }

}
