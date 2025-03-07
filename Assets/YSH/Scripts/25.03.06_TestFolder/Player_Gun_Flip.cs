using UnityEngine;

/// <summary>
/// Scale로 할려 했는데 회전값으로 하는게 맞을 듯?
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