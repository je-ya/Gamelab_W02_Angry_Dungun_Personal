using UnityEngine;

public class PlayerRelicRotate : MonoBehaviour
{
    float _relicRotateSpeed = 200f;

    void Update()
    {
        transform.Rotate(0, 0, _relicRotateSpeed * Time.deltaTime);
    }
}
