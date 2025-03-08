using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm_Move : MonoBehaviour
{
    //public Transform player; // �÷��̾��� Transform
    public float radius = 2f; // ���� ������
    public float speed = 1f; // ���� �׸��� �ӵ�
    private Vector3 centerOfCircle;


    public Vector3 spwanPoint;

    public GameObject followerPrefab;
    public int numFollowers = 10;
    private List<GameObject> followers = new List<GameObject>(); // ���󰡴� ������Ʈ ����Ʈ

    private float angle = 0f;
    private bool isTransitioning = true;
    private float transitionSpeed = 2f; // ���� ��ġ���� ������ �̵��ϴ� �ӵ�
    private float transitionProgress = 0f; // ��ȯ ����� (0 ~ 1)


    private void Start()
    {
        StartCoroutine(CreateFollowers());
        spwanPoint = transform.parent.position;

        float radiusDistence = (radius / 2 + transform.parent.localScale.x);
        centerOfCircle = spwanPoint - new Vector3(radiusDistence, radiusDistence, 0);

        angle = Mathf.Atan2(spwanPoint.y - centerOfCircle.y, spwanPoint.x - centerOfCircle.x); // �ʱ� ���� ���
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
            // ���� ��ġ���� ���� ��η� �̵�
            transitionProgress += transitionSpeed * Time.deltaTime;
            if (transitionProgress >= 1f)
            {
                transitionProgress = 1f;
                isTransitioning = false; // ���� ��� ���� �Ϸ�
            }

            // ���� �ʱ� ��ġ ���
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector3 targetPosition = centerOfCircle + new Vector3(x, y, 0);

            transform.position = Vector3.Lerp(spwanPoint, targetPosition, transitionProgress);
        }
        else
        {
            // �� � ����
            angle -= speed * Time.deltaTime;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            transform.position = centerOfCircle + new Vector3(x, y, 0);
        }

        UpdateFollowers();
    }

    IEnumerator CreateFollowers()
    {
        Transform previousTarget = transform; // ù ��° Ÿ���� ���� �׸��� ������Ʈ
        float interval = 0.1f;

        for (int i = 0; i < numFollowers; i++)
        {
            // ���󰡴� ������Ʈ ����
            GameObject follower = Instantiate(followerPrefab, transform.position, Quaternion.identity);

            follower.transform.parent = transform;
            Worm_circle followerScript = follower.GetComponent<Worm_circle>();

            // Follower ��ũ��Ʈ ����
            followerScript.target = transform;

            SetOrderInLayer(follower, 9);


            // ����Ʈ�� �߰�
            //followers.Add(follower);

            // ���� ������Ʈ�� Ÿ���� ���� ������Ʈ�� ����
            //previousTarget = follower.transform;
            yield return new WaitForSeconds(interval);
        }
    }


    void UpdateFollowers()
    {
        // ��� ���󰡴� ������Ʈ ������Ʈ
        foreach (var follower in followers)
        {
            Worm_circle followerScript = follower.GetComponent<Worm_circle>();
            followerScript.UpdatePosition();
        }
    }

    void SetOrderInLayer(GameObject obj, int orderInLayer)
    {
        // SpriteRenderer ������Ʈ�� �ִ��� Ȯ��
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Order in Layer ����
            spriteRenderer.sortingOrder = orderInLayer;
        }
    }

    private void OnDestroy()
    {
        // ��� �ȷο� ������Ʈ �ı�
        foreach (var follower in followers)
        {
            if (follower != null) // �ȷο��� ���� �����ϴ��� Ȯ��
            {
                Destroy(follower);
            }
        }
        followers.Clear(); // ����Ʈ ����
    }



}