using UnityEngine;

/// <summary>
/// Scale�� �ҷ� �ߴµ� ȸ�������� �ϴ°� ���� ��?
/// </summary>
public class Player_Gun_Flip : MonoBehaviour
{
    private float _gunRotateZ; 

    void Update()
    {
        _gunRotateZ = transform.localEulerAngles.z;

        Debug.Log("Gun Z Rotation: " + _gunRotateZ);

        if (_gunRotateZ >= 0 && _gunRotateZ <= 180)
            transform.localScale = new Vector3(0.25f, 0.25f, 0.25f); 
        else
            transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f); 
    }
}