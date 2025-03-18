using UnityEngine;


//캐릭터가 일정 거리를 움직이면 스트레스가 증가하는 이벤트가 발생하게 함
//디버그 로그가 있는 칸에서 해당 캐릭터의 스트레스 수치를 줄이는 함수 호출
public class MoveStress : MonoBehaviour
{
    float playerLastXPoint;
    float threshold = 5f;
    Character character;
    int damage;

    void Start()
    {
        init();

    }

    void init()
    {
        playerLastXPoint = Mathf.Floor(transform.position.x / threshold) * threshold;
        damage = Random.Range(5, 9);
        character = GetComponent<Character>();
    }

    void Update()
    {
        float currentXpoint = Mathf.Floor(transform.position.x / threshold) * threshold;

        if (currentXpoint != playerLastXPoint)
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
        if (chance <= increaseProbability)
        {
            character.TakeSDamage(damage);
            Debug.Log($"[{gameObject.name}]스트레스 {damage}증가!!");
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
            character.TakeSDamage(damage);
            Debug.Log($"[{gameObject.name}]스트레스 {damage}증가!");
            return true;
        }
        return false;
    }

}

