using UnityEngine;

/// <summary>
/// 아이템 데이터를 가져오기 위한 방식으로 사용된 ScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject / Item")]
public class Item_Data : ScriptableObject
{
    public Sprite _itemImage;
    public int _itemID;
    public float healing; // 체력 회복 용 
    public float _shootSpeedUp; // 탄환 나가는 속도 증가 
    public float _speedUp; // 플레이어 이동 속도 증가 

}
