using UnityEngine;

public class MoveStress : MonoBehaviour
{
    float playerLastXPoint;
    float threshold = 5f;

    void Start()
    {
        init();
    }

    void init()
    {
        playerLastXPoint = Mathf.Floor(transform.position.x / threshold) * threshold;
    }

    void Update()
    {
        float currentXpoint = Mathf.Floor(transform.position.x / threshold) * threshold;
        
        if(currentXpoint != playerLastXPoint)
        { 
            //Debug.Log($"[{gameObject.name}] 이동 감지 - 이전: {playerLastXPoint}, 현재: {currentXpoint}");    //스트레스 포인트 갱신 확인 용도
            if (currentXpoint > playerLastXPoint) // x+ 방향 이동
            {
                //필요한 동작 실행
                ForwardStressUp();
                
            }
            else if (currentXpoint < playerLastXPoint)
            {
                backwardStressUp();
            }
            playerLastXPoint = currentXpoint;
        }
    }
    public bool ForwardStressUp()
    {
        float increaseProbability = 15f;
        float chance = Random.Range(0f, 100f);
        if( chance <= increaseProbability)
        {
            Debug.Log($"[{gameObject.name}]스트레스 증가!");
            return true;
        }
        return false;
    }


    public bool backwardStressUp()
    {
        float increaseProbability = 35f;
        float chance = Random.Range(0f, 100f);
        if (chance <= increaseProbability)
        {
            Debug.Log($"[{gameObject.name}]스트레스 증가!");
            return true;
        }
        return false;
    }

}

