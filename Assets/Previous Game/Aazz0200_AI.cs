using UnityEngine;

public class Aazz0200_AI : MonoBehaviour
{
    public GameObject p;

    public bool is_Look_Target = true;


    void Start()
    {
        p = FindObjectOfType<Aazz0200_Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Look_Target)
        {
            Vector3 dir = p.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }


    }

    public void Fwd_player_Rnd(float f)
    {
        Vector3 p2 = p.transform.position;
        p2.x += Random.RandomRange(-f, f);
        p2.y += Random.RandomRange(-f, f);


        Vector3 dir = p2 - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


    }
}
