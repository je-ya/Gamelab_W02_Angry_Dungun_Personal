using UnityEngine;

/// <summary>
/// 보스 총알이 날라가면서 회전하는 코드 
/// </summary>
public class BossBulletRotate : MonoBehaviour
{
    float _bossBulletRotateSpeed = 200f; // Boss총알 회전 속도 

    void Update()
    {
        transform.Rotate(0, 0, 1 * _bossBulletRotateSpeed * Time.deltaTime); //속도를 늘리면 될듯?
    }
}
