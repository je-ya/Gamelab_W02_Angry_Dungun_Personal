using UnityEngine;

/// <summary>
/// �÷��̾�� Ư�� �Ÿ� �ȿ� �ִٸ� �ı��ǵ��� ���� 
/// �׸��� �ش� �̹��� ������ Relic���� ���� �ص� �ɵ�
/// </summary>
public class RelicDestroy : MonoBehaviour
{
    GameObject _player; // player ������ �޾ƿ;� �� 
    float _destroyDir = 1f; // �νĵǴ� �Ÿ� (����� �� �پ� �ִ°� ���⵵ �� )

    int _relicID; // ���� Relic�� ���̵� 

    void Start()
    {
        _player = GameObject.FindAnyObjectByType<Aazz0200_Player>().gameObject;
        _relicID = GetComponent<RelicID>()._RelicID;
        Debug.Log(_relicID);

    }

    void Update()
    {
        if (_player == null)
            return;

        float dir = ((Vector2)transform.position - (Vector2)_player.transform.position).magnitude;

        if (dir > _destroyDir)
            return;

        Debug.Log("�νĵǾ����ϴ�.");
        RelicGetEvent.Instance.RelicGet(_relicID);
        Destroy(gameObject);
    }
}
