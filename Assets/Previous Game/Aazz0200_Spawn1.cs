using UnityEngine;

public class Aazz0200_Spawn1 : MonoBehaviour
{
    public GameObject pref;
    public float range = 30;


    public void Spawn()
    {
        //������ġ 
        Vector3 p = transform.position;


        p.x += Random.RandomRange(-range, range);
        p.y += Random.RandomRange(-range, range);


        //�÷��̾� �ٶ󺸱�
        Vector3 dir = FindObjectOfType<Aazz0200_Player>().gameObject.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject go = Instantiate(pref, p, Quaternion.AngleAxis(angle - 90, Vector3.forward));

    }



}

/*
 
 
      Vector3 p = transform.position;
        p.x += Random.RandomRange(-10, 010);
        p.y += Random.RandomRange(-10, 010);

        Vector3 d = p - transform.position; d.Normalize();
        Vector3 p2 = transform.position + d * radius;
 
 
 
 
 
 */