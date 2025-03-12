using UnityEngine;

/// <summary>
/// 플레이어와 특정 거리 안에 있다면 파괴되도록 설정 
/// 그리고 해당 이미지 연결을 Relic으로 연결 해도 될듯
/// </summary>
public class RelicDestroy : MonoBehaviour
{
    GameObject _player; // player 참조를 받아와야 함 
    float _destroyDir = 1f; // 인식되는 거리 (현재는 좀 붙어 있는거 같기도 함 )

    int _relicID; // 현재 Relic의 아이디 

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

        Debug.Log("인식되었습니다.");
        RelicGetEvent.Instance.RelicGet(_relicID);
        Destroy(gameObject);
    }
}
