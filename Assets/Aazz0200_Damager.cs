using UnityEngine;

public class Aazz0200_Damager : MonoBehaviour
{
    public float val;
    public Team team;

    public UnityEngine.Events.UnityEvent OnHit;




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(2);
        var l = collision.gameObject.GetComponent<Aazz0200_Life>();
        if (l != null)
        {
            if (l.team != team)
            {
                l.Hit(val);
                OnHit.Invoke();
            }
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        var l = collision.gameObject.GetComponent<Aazz0200_Life>();
        if (l != null)
        {
            if (l.team != team)
            {
                l.Hit(val);
                OnHit.Invoke();
            }

        }
    }


    public void Dest() {   
        Destroy(gameObject); }

}




/*   // private void OnTriggerEnter2D(Collider2D collision)
   // {
   //     Debug.Log(1);
   //      var l =  collision.GetComponent<Aazz0200_Life>();
   //     l.Hit(val);
   // }
*/