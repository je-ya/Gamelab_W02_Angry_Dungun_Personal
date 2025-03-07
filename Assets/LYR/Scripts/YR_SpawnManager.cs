using UnityEngine;
using System.Collections;


public class YR_SpawnManager : MonoBehaviour
{

    public Worm_Forward wormMoveScript;


    public GameObject bossObject;
    private float bossSpawnDelay = 1f;
    public GameObject enemy_nomal;

    public float minRadius = 3f; // �ּ� ���� �ݰ�
    public float maxRadius = 5f; // �ִ� ���� �ݰ�
    public float spawnDelay = 5f; // ���� ���� �� ���� ���۱����� ������
    public float spawnInterval = 0.5f; // ������Ʈ ���� ����


    [SerializeField]
    private Vector3[] bossSpawnPositions = new Vector3[]
    {

    };

    [System.Serializable]
    public struct SpawnData
    {
        public GameObject enemyType; // ������ ������Ʈ
        public Vector3 spawnPoint;       // ���� ��ġ (Vector3�� ����)
        public Quaternion rotation;
        public Worm_Forward.PlayerState state;
    }

    public SpawnData spawnData;

    [SerializeField]
    private SpawnData[] enemyList;

    private Transform playerTransform;


    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Worm_Forward wormForward = spawnData.enemyType.GetComponentInChildren<Worm_Forward>();
        wormForward.currentState = spawnData.state;
        SpawnAllEnemies();
        Invoke("BossSpawn", bossSpawnDelay);

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        // �ʱ� ������
        yield return new WaitForSeconds(spawnDelay);

        while (gameObject.activeInHierarchy)
        {
            // �÷��̾� ��ġ�� �������� ������ ��ġ ���
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y,0 );
            if (spawnPosition.x > -130 && spawnPosition.x < -70 && spawnPosition.y > -30 && spawnPosition.y < 30)
            {            // ������Ʈ ����
                var v = Instantiate(enemy_nomal, spawnPosition, Quaternion.identity);
                v.transform.parent = transform;

                v.transform.position = new Vector3(v.transform.position.x, v.transform.position.y, 0);


                // ���� �������� ���
                yield return new WaitForSeconds(spawnInterval);
            }

        }
    }


    void BossSpawn()
    {
       // // �� ��ġ �� �ϳ��� �������� ����
       // int randomIndex = UnityEngine.Random.Range(0, 2); // 0 �Ǵ� 1
       // Vector3 spawnPosition = bossSpawnPositions[randomIndex];
       //
       // Vector3 worldSpawnPositionB = transform.TransformPoint(spawnPosition);
       // //���� ��ġ�� ���� ��ġ�� ��ȯ
       //
       // Instantiate(bossObject, worldSpawnPositionB, Quaternion.identity);
       // // ������Ʈ�� ���� ��ǥ�� ����
    }



    private void SpawnAllEnemies()
    {
        if (enemyList == null || enemyList.Length == 0)
        {
            Debug.LogWarning("Enemy list is empty or null!");
            return;
        }

        foreach (SpawnData spawnData in enemyList)
        {
            // enemyType�� null���� Ȯ��
            if (spawnData.enemyType == null)
            {
                Debug.LogWarning("Enemy type is null in spawn data!");
                continue;
            }


            Vector3 worldSpawnPositionE = transform.TransformPoint(spawnData.spawnPoint);
            Quaternion worldSpawnRotationE = transform.rotation * spawnData.rotation;

          var v=  Instantiate(spawnData.enemyType, worldSpawnPositionE, worldSpawnRotationE);
            v.transform.parent = transform;

            v.transform.position = new Vector3(v.transform.position.x, v.transform.position.y, 0);
            // ������Ʈ�� ���� ��ǥ�� ����(�ν��Ͻ�ȭ)
        }
    }

}