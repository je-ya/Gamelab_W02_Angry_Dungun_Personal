using UnityEngine;
using System.Collections;


public class YR_SpawnManager : MonoBehaviour
{

    public Worm_Forward wormMoveScript;


    public GameObject bossObject;
    private float bossSpawnDelay = 1f;
    public GameObject enemy_nomal;



    public float minRadius = 3f; // 최소 스폰 반경
    public float maxRadius = 5f; // 최대 스폰 반경
    public float spawnDelay = 1f; // 게임 시작 후 스폰 시작까지의 딜레이
    public float spawnInterval = 0.5f; // 오브젝트 스폰 간격



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
        public GameObject enemyType; // 스폰할 오브젝트
        public Vector3 spawnPoint;       // 스폰 위치 (Vector3로 변경)
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
    
    //플레이어를 따라다니는 기본 몹 스폰
    IEnumerator SpawnnomalWorm()
    {
        // 초기 딜레이
        yield return new WaitForSeconds(spawnDelay);

        while (gameObject.activeInHierarchy)
        {
            // 플레이어 위치를 기준으로 랜덤한 위치 계산
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, randomCircle.y,0 );
            //Debug.Log(playerTransform.position);
/*
            if (playerTransform.position.x > xMin && playerTransform.position.x < xMax &&
                playerTransform.position.y > yMin && playerTransform.position.y < yMax)
            {
                Debug.Log("플레이어가 직사각형 영역 안에 있습니다!");
*/
                if (spawnPosition.x > xMin && spawnPosition.x < xMax && spawnPosition.y > yMin && spawnPosition.y < yMax)
                {            // 오브젝트 스폰
                    var v = Instantiate(enemy_nomal, spawnPosition, Quaternion.identity);
                    v.transform.parent = transform;

                    v.transform.position = new Vector3(v.transform.position.x, v.transform.position.y, 0);


                    // 다음 스폰까지 대기
                    
                }
/*
            }
            else
            {
                Debug.Log("플레이어가 스폰 범위 내에 없음");
            }*/
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    void BossSpawn()
    {
       // // 두 위치 중 하나를 랜덤으로 선택
       // int randomIndex = UnityEngine.Random.Range(0, 2); // 0 또는 1
       // Vector3 spawnPosition = bossSpawnPositions[randomIndex];
       //
       // Vector3 worldSpawnPositionB = transform.TransformPoint(spawnPosition);
       // //로컬 위치를 월드 위치로 변환
       //
       // Instantiate(bossObject, worldSpawnPositionB, Quaternion.identity);
       // // 오브젝트를 월드 좌표에 스폰
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
            // enemyType이 null인지 확인
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
            // 오브젝트를 월드 좌표에 스폰(인스턴스화)
        }
    }

}