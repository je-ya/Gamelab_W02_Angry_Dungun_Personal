using System.Collections;
using UnityEngine;

public class Aazz0200_Timer : MonoBehaviour
{
    public float time;
    public bool isRepeat;
    public float isRnd = 0;
    public UnityEngine.Events.UnityEvent OnTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Act());

    }

    IEnumerator Act()
    {
        if (isRepeat)
        {
            for (; ; )
            {
                float f = time;
                if (isRnd != 0) f = Random.RandomRange(isRnd, time);

                yield return new WaitForSeconds(f);
                OnTime.Invoke();
            }
        }
        else
        {
            float f = time;
            if (isRnd != 0) f = Random.RandomRange(isRnd, time);


            yield return new WaitForSeconds(f);
            OnTime.Invoke();
        }
    }

    public void SeparateParent() { transform.parent = null; }
    public void Destme() { Destroy(gameObject); }
    public void Timescale(float v) { Time.timeScale = v; }
    public void Inst(GameObject go) { Instantiate(go, transform.position, transform.rotation); }
}


