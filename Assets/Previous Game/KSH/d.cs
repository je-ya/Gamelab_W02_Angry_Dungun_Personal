
using UnityEngine;

public class d : MonoBehaviour
{
    public float v = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * v * Time.deltaTime;
    }
}
