using UnityEngine;

public class KHW_BossBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    [SerializeField] Transform p;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void Launch(Transform playerTransform)
    {
        p = playerTransform;

        Vector2 launchDirection = (playerTransform.position - transform.position).normalized;

        rb.AddForce(launchDirection * speed, ForceMode2D.Impulse);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
