using System.Collections;
using UnityEngine;

public class dd : MonoBehaviour
{
    public GameObject ddd;
    public Alter[] at;

    void Start()
    {
        at = FindObjectsOfType<Alter>(true);
    }

    private void Update()
    {
        float count = 0;
        for (int i = 0; i < at.Length; i++)
        {
            if (at[i].gameObject.active == true)
                count++;
        }


        if (count >= 3)
        {
            StartCoroutine(dddd());


        }
    }
    public void Boss_room()
    {
        FindObjectOfType<Aazz0200_Player>().transform.position = new Vector3(0, 200, 0);
        Camera.main.transform.position = new Vector3(0, 200, 0);
    }


    IEnumerator dddd()
    {

        yield return new WaitForSeconds(0);

       if(ddd) ddd.SetActive(true);

    }


}
