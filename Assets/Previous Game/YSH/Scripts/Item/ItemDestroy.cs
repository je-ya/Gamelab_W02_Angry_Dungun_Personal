using UnityEngine;

/// <summary>
/// Relic�� �ƴ� Item �ı� 
/// </summary>
public class ItemDestroy : MonoBehaviour
{
    GameObject _player; // player ������ �޾ƿ;� �� 
    float _destroyDir = 0.5f; // �νĵǴ� �Ÿ� (����� �� �پ� �ִ°� ���⵵ �� )

    int _itemID; // ���� Item�� ���̵� 


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

        Debug.Log("�νĵǾ����ϴ�.");
        // �ش� �κп� Action ���� �ؼ� ���� ���ָ� �ɵ�? 
        ItemGetEventManager.Instance.GetItem(_itemID); // �ش� �������� �̺�Ʈ�� �Ѱ��ִ°ǵ� RelicEventmanager�� ���� ���� ó�� ����� �ҵ�?
        Destroy(gameObject);
    }
}
