using System;
using UnityEngine;

/// <summary>
/// �����Ѿ� �ı� �Ǿ��� ������ ��������� Action ���� 
/// </summary>
public class BossBigBulletDestroyManager : MonoBehaviour
{
    public static BossBigBulletDestroyManager Instance => _instance;
    static BossBigBulletDestroyManager _instance;

    public Action OnBigBulletDestroyEvent; // ���� ū �Ѿ� �ı� �̺�Ʈ �����

    private void Awake()
    {
        _instance = this;
    }
    // �̰� �Լ��� �����ϴ� �κ� �ı��Ǹ� �����Ҳ� 
    public void BossBigBulletDestroy()
    {
        OnBigBulletDestroyEvent?.Invoke();
    }
}
