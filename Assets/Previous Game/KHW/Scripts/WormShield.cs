using UnityEngine;

public class WormShield : MonoBehaviour
{
    [SerializeField] Aazz0200_Act act;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Destroy(collision.gameObject);

            Debug.Log("¹æÆÐ »ç¿ëµÊ!");

            act.disableWormShieldState();
        }


    }
}
