using System.Collections;
using UnityEngine;

public class KHW_SpawnManager : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] float centerPosX = 80f;
    [SerializeField] float centerPosY = 0f;
    [SerializeField] float maxX = 18f;
    [SerializeField] float maxY = 16f;

    [Header("References")]
    [SerializeField] GameObject razor; //razor + warningLine.


    [Header("CoolDowns")]
    //CoolDowns.
    [SerializeField] float normalRazorCoolDown = 3f;
    [SerializeField] float rotatingRazorCoolDown = 10f;
    [SerializeField] float enemySpawnCoolDown = 5f;

    //elapsedTimes.
    [SerializeField] float normalRazorElapsedTime = 0f;
    [SerializeField] float rotatingRazorElapsedTime = 0f;
    [SerializeField] float enemySpawnElapsedTime = 0f;

    [SerializeField] float normalRazorTimeReducing = 0.1f;
    [SerializeField] float rotatingRazorTimeReducing = 0.1f;
    [SerializeField] float enemySpawnTimeReducing = 0.1f;

    [Header("Normal Razor")]

    [Header("RotatingRazor")]
    [SerializeField] float rotatingRazorSpawnInterval = 0.4f;
    [SerializeField] float rotationIncrement = 15f;
    [SerializeField] int rotatingRazorSpawnCount = 0;
    [SerializeField] bool isRotatingRazorTriggered = false;


    //PhaseCounting.
    [Header("PhaseCounts")]
    [SerializeField] int maxPhaseCount = 1;
    public int phaseCount = 0;

    void Start()
    {

    }

    void Update()
    {
        UpdateElapsedTime();

        if (normalRazorElapsedTime >= normalRazorCoolDown)
        {
            NormalRazor();
            normalRazorElapsedTime = 0;
        }
        if (rotatingRazorElapsedTime >= rotatingRazorCoolDown && !isRotatingRazorTriggered)
        {
            RotatingRazor();
            rotatingRazorElapsedTime = 0;
        }
        if (enemySpawnElapsedTime >= enemySpawnCoolDown)
        {
            //SpawnEnemy();
            //enemySpawnElapsedTime = 0;
        }
    }

    void UpdateElapsedTime()
    {
        normalRazorElapsedTime += Time.deltaTime;
        rotatingRazorElapsedTime += Time.deltaTime;
        enemySpawnElapsedTime += Time.deltaTime;
    }


    //�Ϲ� ������
    void NormalRazor()
    {
        bool isVertical = Random.Range(0, 2) == 1;

        if (isVertical)
        {
            float posX = Random.Range(centerPosX - maxX, centerPosX + maxX);

            Vector2 razorSpawnPosition = new Vector2(posX, centerPosY);
            Quaternion razorSpawnRotation = Quaternion.Euler(0, 0, 90);

            Instantiate(razor, razorSpawnPosition, razorSpawnRotation);
        }
        else
        {
            float posY = Random.Range(centerPosY -maxY, centerPosY + maxY);
            Vector2 razorSpawnPosition = new Vector2(centerPosX, posY);

            Instantiate(razor, razorSpawnPosition, Quaternion.identity);
        }

        if (normalRazorCoolDown > 1.1f)
        {
            normalRazorCoolDown -= normalRazorTimeReducing;
        }

        phaseCount++;
    }

    //�� ���� ����
    void RotatingRazor()
    {
        isRotatingRazorTriggered = true;
        StartCoroutine(SpawnRotatingRazor());
    }

    private IEnumerator SpawnRotatingRazor()
    {
        float currentAngle = Random.Range(0f, 360f);

        rotatingRazorSpawnCount = phaseCount;

        for (int i = 0; i < rotatingRazorSpawnCount; i++)
        {
            Vector2 spawnPosition = new Vector2(centerPosX, centerPosY);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, currentAngle); // ���� ������ ȸ��

            // ������ ����
            GameObject spawnedRazor = Instantiate(razor, spawnPosition, spawnRotation);

            // ���� ������ ���� ���� ����
            currentAngle += rotationIncrement;

            // spawnInterval��ŭ ���
            yield return new WaitForSeconds(rotatingRazorSpawnInterval);
        }

        // �ڷ�ƾ ���� �� ��ٿ� �� ��� �ð� ����
        if (rotatingRazorCoolDown > 5f)
        {
            rotatingRazorCoolDown -= rotatingRazorTimeReducing;
        }

        rotatingRazorElapsedTime = 0f;
        isRotatingRazorTriggered = false;
    }

    //�� ���� ����

}
