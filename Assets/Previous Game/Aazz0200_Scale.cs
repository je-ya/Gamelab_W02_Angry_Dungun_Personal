using UnityEngine;

public class Aazz0200_Scale : MonoBehaviour
{
    public float val = 1;
    public float dist = 20;

    void Start()
    {
        var pl = FindObjectOfType<Aazz0200_Player>().gameObject;

        Vector3 p = pl.transform.position;
        p.y += Random.RandomRange(-dist, dist);
        p.x += Random.RandomRange(-dist, dist);

        transform.position = p;

    }
    // Update is called once per frame
    void Update()
    {

        // transform.localScale += transform.localScale * Time.deltaTime * val;
        transform.localScale += Vector3.one * Time.deltaTime * val;
    }
}
