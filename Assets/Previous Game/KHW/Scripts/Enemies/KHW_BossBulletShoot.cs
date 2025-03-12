using UnityEngine;

public class KHW_BossBulletShoot : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject projectilePrefab;
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        InvokeRepeating("ShootBullet", 2f, 3f);
    }

    void ShootBullet()
    {
        if (playerTransform != null)
        {
            GameObject newBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<KHW_BossBullet>().Launch(playerTransform);
        }

    }

    private void OnDestroy()
    {
        CancelInvoke("ShootBullet");
    }
}
