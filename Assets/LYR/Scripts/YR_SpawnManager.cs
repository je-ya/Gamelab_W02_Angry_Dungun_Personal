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
    public float spawnDelay = 1f; // ���� ���� �� ���� ���۱����� ������
    public float spawnInterval = 0.5f; // ������Ʈ ���� ����



    float xMin;
    float xMax;
    float yMin;
    float yMax;

    CheckCenterOfMap centerOb;
    Vector3 centerPosion;
    float radius = 39f;


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


    private void Awake()
    {
        Worm_Forward wormForward = spawnData.enemyType.GetComponentInChildren<Worm_Forward>();
        wormForward.currentState = spawnData.state;
    }
    private void Start()
    {
        //centerOb = GameObject.FindAnyObjectByType<CheckCenterOfMap>();
        //centerPosion = centerOb.GetComponent<Transform>().position;

        centerPosion = FindAnyObjectByType<CheckCenterOfMap>().transform.position;



        xMax = centerPosion.x + radius;
        xMin = centerPosion.x - radius;
        yMax = centerPosion.y + radius;
        yMin = centerPosion.y - radius;
        Debug.Log("xMin: " + xMin + ", xMax: " + xMax + ", yMin: " + yMin + ", yMax: " + yMax);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;


        SpawnAllEnemies();
        Invoke("BossSpawn", bossSpawnDelay);


        StartCoroutine(SpawnnomalWorm());
    }
    
    //�÷��̾ ����ٴϴ� �⺻ �� ����
    IEnumerator SpawnnomalWorm()
    {
        // �ʱ� ������
        yield return new WaitForSeconds(spawnDelay);

        while (gameObject.activeInHierarchy)
        {
            // �÷��̾� ��ġ�� �������� ������ ��ġ ���
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y,0 );
            //Debug.Log(playerTransform.position);
/*
            if (playerTransform.position.x > xMin && playerTransform.position.x < xMax &&
                playerTransform.position.y > yMin && playerTransform.position.y < yMax)
            {
                Debug.Log("�÷��̾ ���簢�� ���� �ȿ� �ֽ��ϴ�!");
*/
                if (spawnPosition.x > xMin && spawnPosition.x < xMax && spawnPosition.y > yMin && spawnPosition.y < yMax)
                {            // ������Ʈ ����
                    var v = Instantiate(enemy_nomal, spawnPosition, Quaternion.identity);
                    v.transform.parent = transform;

                    v.transform.position = new Vector3(v.transform.position.x, v.transform.position.y, 0);


                    // ���� �������� ���
                    
                }
/*
            }
            else
            {
                Debug.Log("�÷��̾ ���� ���� ���� ����");
            }*/
            yield return new WaitForSeconds(spawnInterval);
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