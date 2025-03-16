using UnityEngine;

//플레이어를 따라가는 카메라와, 카메라 오프셋 설정
public class PlayerCamera : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public Vector3 offset;
    public GameObject player;




    void Update()
    {
        Vector3 to = player.transform.position + offset;

        transform.position = player.transform.position + offset;



    }
}
