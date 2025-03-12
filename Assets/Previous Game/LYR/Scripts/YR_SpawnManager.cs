using System.Collections;
using UnityEngine;




public class YR_SpawnManager : MonoBehaviour
{

    public GameObject boss;
    public Vector3[] bossSpawnPositions = new Vector3[]
    {

    };
    float bossSpawnDelay = 1f;

    [System.Serializable]
    public struct SpawnData
    {
        public GameObject enemyType; // 스폰할 오브젝트
        public Vector3 spawnPoint;       // 스폰 위치 (Vector3로 변경)
        public Quaternion rotation;
        public Worm_Forward.PlayerState state;
    }

    public GameObject nomal_enemy;
    Transform playerTransform;
    public float minRadius_N = 30f;
    public float maxRadius_N = 50f;
    float spawnDelay_N = 1f;
    float spawnInterval_N = 0.5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Vector3 centerPosion;
    float radius = 39f;


    

    [SerializeField]
    private SpawnData[] enemyList;



    private void Start()
    {
        init();

        SpawnListEnemies();
        //Invoke("BossSpawn", bossSpawnDelay);
        StartCoroutine(SpawnnomalWorm());
    }

    void init()
    {
        centerPosion = FindAnyObjectByType<CheckCenterOfMap>().transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        xMax = centerPosion.x + radius;
        xMin = centerPosion.x - radius;
        yMax = centerPosion.y + radius;
        yMin = centerPosion.y - radius;
        Debug.Log("xMin: " + xMin + ", xMax: " + xMax + ", yMin: " + yMin + ", yMax: " + yMax);

    }

    void BossSpawn()
    {
        // 두 위치 중 하나를 랜덤으로 선택
        int randomIndex = UnityEngine.Random.Range(0, 2); // 0 또는 1
        Vector3 spawnPosition = bossSpawnPositions[randomIndex];

        Vector3 worldSpawnPositionB = transform.TransformPoint(spawnPosition);
        //로컬 위치를 월드 위치로 변환

        Instantiate(boss, worldSpawnPositionB, Quaternion.identity);
        // 오브젝트를 월드 좌표에 스폰
    }


    void SpawnListEnemies()
    {
        if (enemyList == null || enemyList.Length == 0)
        {
            Debug.Log("Enemy list is empty!");
            return;
        }

        foreach (SpawnData spawnData in enemyList)
        {
            if (spawnData.enemyType != null)
            {
                Vector3 worldSpawnPoint = transform.TransformPoint(spawnData.spawnPoint);

                Quaternion worldSpawnRotate = transform.rotation * spawnData.rotation;

                GameObject spawnedEnemy = Instantiate(spawnData.enemyType, worldSpawnPoint, worldSpawnRotate);

                Worm_Forward fwdMovescript = spawnedEnemy.transform.GetChild(0).GetComponent<Worm_Forward>();
                spawnedEnemy.transform.parent = transform;
                spawnedEnemy.transform.position = new Vector3(spawnedEnemy.transform.position.x, spawnedEnemy.transform.position.y, 0);

                if (fwdMovescript != null)
                {
                    fwdMovescript.currentState = spawnData.state;
                    Debug.Log(spawnData.state);
                }
                else
                {
                    Debug.LogWarning($"Spawned enemy {spawnedEnemy.name} does not have Worm_Forward component!");
                }
            }
            else
            {
                Debug.LogWarning("Enemy type is not assigned in spawn data!");
            }
        }
    }



    IEnumerator SpawnnomalWorm()
    {
        yield return new WaitForSeconds(spawnDelay_N);
        
        while(gameObject.activeInHierarchy)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minRadius_N, maxRadius_N);
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y, 0);

            if(spawnPosition.x > xMin && spawnPosition.x < xMax && spawnPosition.y > yMin && spawnPosition.y < yMax)
            {
                var insideParent = Instantiate(nomal_enemy, spawnPosition, Quaternion.identity);
                insideParent.transform.parent = transform;

                insideParent.transform.position = new Vector3(insideParent.transform.position.x, insideParent.transform.position.y, 0);
            }
            else
            {
                Debug.Log("플레이어가 스폰 범위 내에 없음");
            }
            
            yield return new WaitForSeconds(spawnInterval_N);
        }
    }    


    }


