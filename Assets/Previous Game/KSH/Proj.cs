using System.Collections;
using UnityEngine;

public class Proj : MonoBehaviour
{
    public Aazz0200_Player p;

    public float speed;
    public float dist_max;
    public float break2 = 3;

    public Vector3 all;
    public Coroutine c_accel;
    Vector3 dir;
    bool isFly;


    void Start()
    {
        p = FindObjectOfType<Aazz0200_Player>();
        c_accel = StartCoroutine(Accel());
        isFly = true;
    }


    void Update()
    {
        transform.position += all * Time.deltaTime;



        //°Å¸® ¸Ö¾îÁü  

        if (isFly == true)
        {
            if (Vector3.Distance(p.transform.position, transform.position) >= dist_max)
            {
                isFly = false;
                if (c_accel != null) { StopCoroutine(c_accel); c_accel = null; }
                StartCoroutine(Break(new Vector3(all.x, all.y, all.z)));
                all = Vector3.zero;

                c_accel = StartCoroutine(Accel());
                dir = p.transform.position - transform.position;
                //Invoke("Fly",1);
            }
        }
        else
        {

            if (Vector3.Distance(p.transform.position, transform.position) < dist_max)
                isFly = true;


        }
    }
    void Fly() { isFly = true; }


    IEnumerator Break(Vector3 o)
    {
        for (; ; )
        {
            o = Vector3.Lerp(o, default, break2 * Time.deltaTime);
            transform.position += o * Time.deltaTime;

            if (o.magnitude < 0.1f)
                break;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator Accel()
    {
        for (; ; )
        {
            Vector3 dir2 = p.transform.position - transform.position;
            all += dir2.normalized * speed * Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
/*
   if (c_accel != null)
                //StopCoroutine(c_accel); c_accel = null;
                //StartCoroutine(Break(new Vector3(all.x, all.y, all.z)));
                //all = Vector3.zero;
                //
                //c_accel = StartCoroutine(Accel());
         all += (p.transform.position - transform.position).normalized * speed 
            * Time.deltaTime;
 */
//  all = all.normalized * max;
// var v = Vector3.Distance(p.transform.position, transform.position);

//transform.rotation = Quaternion.Slerp(transform.rotation, 
//    Quaternion.LookRotation( p.transform.position - transform.position), speed * Time.deltaTime);