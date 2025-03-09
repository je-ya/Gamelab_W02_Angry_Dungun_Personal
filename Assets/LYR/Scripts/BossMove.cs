using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    private Vector3 targetPosition; // 목적지 위치
    private bool isMoving = false; // 이동 중인지 여부
    private bool isWaiting = false; // 대기 중인지 여부'
    CheckCenterOfMap centerOb;
    Vector3 centerPosion;
    float radius = 33;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        //MapCenter 오브젝트의 위치 가져오기
        centerOb = GameObject.FindAnyObjectByType<CheckCenterOfMap>();
        centerPosion = centerOb.GetComponent<Transform>().position;
        //Debug.Log(centerPosion); //재대로 Center 위치가 나오는지 확인

        xMax = centerPosion.x + radius;
        xMin = centerPosion.x - radius;
        yMax = centerPosion.y - radius;
        yMin = centerPosion.y + radius;


        // 초기 목적지 설정
        SetNewTargetPosition();

        // 10초 후에 출발
        StartCoroutine(StartMovingAfterDelay(10f));
    }

    void Update()
    {
        if (isMoving && !isWaiting)
        {
            // 목적지로 이동
            MoveToTarget();
        }
    }

    void SetNewTargetPosition()
    {
        //하드코딩 한 부분 - Center 오브젝트 만들고, 그 오브젝트값 가져와서 그거 반경 얼마로 수정하면 될 것 같음
        //오브젝트 가져오는걸 저번에 말한 빈 스크립트 이용해도 될까?
        //만약에 Center 오브젝트를 못찾으면 에러 메세지 출력
        //반경은 변수로 만들어서 수정 할 수 있도록, 그리고 public말고 private에 SerializeField해서 스크립트에서 수치는 체크 가능하도록
        //일단 다 쓰고 난뒤에 이렇게 쓰면 되는건지 물어보자
        
        if (centerPosion != null )
        {
            float randomX = Random.Range(xMax, xMin);
            float randomY = Random.Range(yMax, yMin);
            targetPosition = new Vector3(randomX, randomY, 0);

        }
        else { Debug.Log("Center Ob cannot be found"); }
    }

    void MoveToTarget()
    {
        // 목적지로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목적지에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 도착 후 2초 대기
            StartCoroutine(WaitAtDestination(2f));
        }
    }

    IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true; // 10초 후 이동 시작
    }

    IEnumerator WaitAtDestination(float waitTime)
    {
        isWaiting = true; // 대기 상태 시작
        yield return new WaitForSeconds(waitTime); // 2초 대기

        // 새로운 목적지 설정
        SetNewTargetPosition();
        isWaiting = false; // 대기 상태 종료
    }
}
