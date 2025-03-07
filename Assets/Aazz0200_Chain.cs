using UnityEngine;

public class Aazz0200_Chain : MonoBehaviour
{
    public GameObject follow;
    public float distance;



    void Update()
    {


        if (Vector3.Distance(follow.transform.position, transform.position) > distance)
        {
            Vector3 dir = follow.transform.position - transform.position;
            transform.position = follow.transform.position - dir.normalized * distance;

        }
    }
}
