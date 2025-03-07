using System;
using UnityEngine;

/// <summary>
/// 아이템 먹었을때 발생 시킬 이벤트 형성 
/// </summary>
public class ItemGetEventManager : MonoBehaviour
{
    public static ItemGetEventManager Instance => _instance;
    static ItemGetEventManager _instance;

    /// <summary>
    /// 전달 해줘야 할 아이템의 ItemID를 넘겨줌 Dictionary를 이용해서 ScriptableObject 처리 
    /// </summary>
    public Action<int> OnItemGetEvent;  // 액션 실행 시켰을때 연쇄적으로 들어갈거 

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// 아이템 먹었을때 실행시킬 함수 
    /// </summary>
    /// <param name="itemImage"></param>
    /// <param name="itemID"></param>
    public void GetItem(int itemID)
    {
        OnItemGetEvent?.Invoke(itemID);
    }

}
