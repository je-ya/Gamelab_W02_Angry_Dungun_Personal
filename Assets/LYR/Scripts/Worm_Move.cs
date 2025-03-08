using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm_Move : MonoBehaviour
{
    //public Transform player; // 플레이어의 Transform
    public float radius = 2f; // 원의 반지름
    public float speed = 1f; // 원을 그리는 속도
    private Vector3 centerOfCircle;


    public Vector3 spwanPoint;

    public GameObject followerPrefab;
    public int numFollowers = 10;
    private List<GameObject> followers = new List<GameObject>(); // 따라가는 오브젝트 리스트

    private float angle = 0f;
    private bool isTransitioning = true;
    private float transitionSpeed = 2f; // 시작 위치에서 원으로 이동하는 속도
    private float transitionProgress = 0f; // 전환 진행률 (0 ~ 1)


    private void Start()
    {
        StartCoroutine(CreateFollowers());
        spwanPoint = transform.parent.position;

        float radiusDistence = (radius / 2 + transform.parent.localScale.x);
        centerOfCircle = spwanPoint - new Vector3(radiusDistence, radiusDistence, 0);

        angle = Mathf.Atan2(spwanPoint.y - centerOfCircle.y, spwanPoint.x - centerOfCircle.x); // 초기 각도 계산
    }
    private void OnDisable()
    {

        for (int i = 0; i < followers.Count; i++)
         Destroy  ( followers[i].gameObject);
    }
    void Update()
    {

        if (isTransitioning)
        {
            // 시작 위치에서 원의 경로로 이동
            transitionProgress += transitionSpeed * Time.deltaTime;
            if (transitionProgress >= 1f)
            {
                transitionProgress = 1f;
                isTransitioning = false; // 원의 경로 진입 완료
            }

            // 원의 초기 위치 계산
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector3 targetPosition = centerOfCircle + new Vector3(x, y, 0);

            transform.position = Vector3.Lerp(spwanPoint, targetPosition, transitionProgress);
        }
        else
        {
            // 원 운동 시작
            angle -= speed * Time.deltaTime;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            transform.position = centerOfCircle + new Vector3(x, y, 0);
        }

        UpdateFollowers();
    }

    IEnumerator CreateFollowers()
    {
        Transform previousTarget = transform; // 첫 번째 타겟은 원을 그리는 오브젝트
        float interval = 0.1f;

        for (int i = 0; i < numFollowers; i++)
        {
            // 따라가는 오브젝트 생성
            GameObject follower = Instantiate(followerPrefab, transform.position, Quaternion.identity);

            follower.transform.parent = transform;
            Worm_circle followerScript = follower.GetComponent<Worm_circle>();

            // Follower 스크립트 설정
            followerScript.target = transform;

            SetOrderInLayer(follower, 9);


            // 리스트에 추가
            //followers.Add(follower);

            // 다음 오브젝트의 타겟을 현재 오브젝트로 설정
            //previousTarget = follower.transform;
            yield return new WaitForSeconds(interval);
        }
    }


    void UpdateFollowers()
    {
        // 모든 따라가는 오브젝트 업데이트
        foreach (var follower in followers)
        {
            Worm_circle followerScript = follower.GetComponent<Worm_circle>();
            followerScript.UpdatePosition();
        }
    }

    void SetOrderInLayer(GameObject obj, int orderInLayer)
    {
        // SpriteRenderer 컴포넌트가 있는지 확인
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Order in Layer 설정
            spriteRenderer.sortingOrder = orderInLayer;
        }
    }

    private void OnDestroy()
    {
        // 모든 팔로워 오브젝트 파괴
        foreach (var follower in followers)
        {
            if (follower != null) // 팔로워가 아직 존재하는지 확인
            {
                Destroy(follower);
            }
        }
        followers.Clear(); // 리스트 비우기
    }



}