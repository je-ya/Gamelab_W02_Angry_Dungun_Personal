using UnityEngine;

/// <summary>
/// 인벤토리 슬롯이 비어 있는가를 보고 싶은 코드 
/// 사실상 이미지 비교로 해도 되는데 bool값보다 많이 먹는다고 생각해서 
/// bool값으로 처리 하려 함 
/// </summary>
public class Inventory_IsEmpty : MonoBehaviour
{
    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    }

    public int ItemID
    {
        get { return _itemID; }
        set { _itemID = value; }
    }

    bool _isEmpty = true; // 해당 칸이 비어있다고 이야기를 해주고 
    int _itemID; // 칸에 들어간 아이템의 번호를 넣을까 생각중 -> 그렇다면 해당 방식을 사용하려면 ScriptableObject를 사용하는 것이 가장 좋나?
}
