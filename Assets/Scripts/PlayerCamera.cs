using UnityEngine;

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
