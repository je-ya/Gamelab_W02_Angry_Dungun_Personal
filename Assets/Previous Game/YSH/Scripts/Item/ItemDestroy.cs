using UnityEngine;

/// <summary>
/// Relic이 아닌 Item 파괴 
/// </summary>
public class ItemDestroy : MonoBehaviour
{
    GameObject _player; // player 참조를 받아와야 함 
    float _destroyDir = 0.5f; // 인식되는 거리 (현재는 좀 붙어 있는거 같기도 함 )

    int _itemID; // 현재 Item의 아이디 


    void Start()
    {
        _player = GameObject.FindAnyObjectByType<Aazz0200_Player>().gameObject;
        _itemID = GetComponent<ItemID>()._itemID;
        Debug.Log(_itemID);
    }

    void Update()
    {
        if (_player == null)
            return;

        float dir = ((Vector2)transform.position - (Vector2)_player.transform.position).magnitude;

        if (dir > _destroyDir)
            return;

        Debug.Log("인식되었습니다.");
        // 해당 부분에 Action 연결 해서 전달 해주면 될듯? 
        ItemGetEventManager.Instance.GetItem(_itemID); // 해당 아이템의 이벤트를 넘겨주는건데 RelicEventmanager를 만들어서 따로 처리 해줘야 할듯?
        Destroy(gameObject);
    }
}
