using System.Collections;
using UnityEngine;

public class Aazz0200_Act : MonoBehaviour
{
    [Header("Weapon Prefabs")]
    public GameObject defaultBulletPref; // 기본 총알 
    public GameObject iceRelicBulletPref; // 얼음 유물 프리팹을 가지고 있을때 적용될 총알
    public GameObject wormRelicShieldObj; // 벌레 유물 프리팹을 가지고 있을 때 생성할 방패. Prefab이 아니라 하위 오브젝트의 enable 조절.
    public GameObject lightRelicRayPref; // 빛 유물 프리팹을 가지고 있을 때 생성할 특수 공격.

    public float col_str; // 안쓰더라 
    public float col_max; // 연사속도
    float col_ing; // 연사 속도 시간?

    [Header("Relic Prefabs")]
    public Relic _iceRelic; // 얼음 유물 프리팹 
    public Relic _wormRelic; // 벌레 유물 프리팹 
    public Relic _lightRelic; // 빛 유물 프리팹

    [Header("WormShield References")]
    public float wormShieldCoolDown = 5f;// 총알 막으면 5초뒤에 다시 나오는거 
    [SerializeField] private float elapsedWormShieldTime = 0;
    [SerializeField] private bool isWormShieldEnabled = false;

    [Header("Ray References")]
    public float rayCoolDown = 3f;
    [SerializeField] private float elapsedRayTime = 0f;
    [SerializeField] private bool rayReady = false;
    

    float _nomarlCol = 0.1f; // 아이템 먹었을때 다시 돌아올 연사 속도 

    public float Col_Max
    {

        get { return col_max; }
        set
        {
            col_max = value;
            // 코루틴 사용해서 몇초뒤에 다시 돌아 오면 될듯?
            StartCoroutine(CoolDownShootSpeed());
        }
    }


    // Update is called once per frame
    void Update()
    {
        //================== 충전중 ==================
        if (col_ing < col_max)
        {
            col_ing += Time.deltaTime;

            //과충전
            if (col_ing >= col_max)
                col_ing = col_max;
        }
        //================== 충전중 ==================

        //벌레 방패 관리.
        ManageWormShield();

        //레이저 관리
        ManageRay();
    }

    IEnumerator CoolDownShootSpeed()
    {
        yield return new WaitForSeconds(5f);
        col_max = _nomarlCol;
    }

    public bool Can_Start(GameObject s = null)
    {
        if (col_ing >= col_max)
            return true;

        return false;
    }
    public void Start_Act(GameObject me, Vector3 from, Vector3 to)
    {
        if (Can_Start())
        {
            col_ing = 0;

            GameObject go;
            Vector3 dir = to - me.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


            if (_iceRelic.RelicActive)
            {
                go = Instantiate(iceRelicBulletPref, from, Quaternion.AngleAxis(angle - 90, Vector3.forward));
            }
            else
            {
                go = Instantiate(defaultBulletPref, from, Quaternion.AngleAxis(angle - 90, Vector3.forward));
            }

            if(rayReady)
            {
                Instantiate(lightRelicRayPref, from, Quaternion.AngleAxis(angle, Vector3.forward));

                rayReady = false;
            }
            // prefctr pi = go.GetComponent<prefctr>();
        }
    }


    //벌레 방패 관리
    public void ManageWormShield()
    {
        if(wormRelicShieldObj == null)
        {
            Debug.LogWarning("WormRelicObj 없음. Player(Core) 하위 GameObject여야함.");
        }

        if (_wormRelic == null)
            return;

        if(!_wormRelic.RelicActive)
        {
            return;
        }

        else
        {
            if(!isWormShieldEnabled)
            {
                elapsedWormShieldTime += Time.deltaTime;

                if(elapsedWormShieldTime > wormShieldCoolDown)
                {
                    elapsedWormShieldTime = 0f;
                    wormRelicShieldObj.SetActive(true);
                    isWormShieldEnabled = true;
                }
            }
            

        }
    }

    public void disableWormShieldState() //벌레 방패 끄기.
    {
        wormRelicShieldObj.SetActive(false); //벌레 방패 파괴.

        isWormShieldEnabled = false;

    }

    //빛이여
    private void ManageRay()
    {
        if (_lightRelic == null)
        {
            return;
        }
        if(!_lightRelic.RelicActive) //빛 렐릭 없음
        {
            return;
        }
        else
        {
            if(!rayReady)
            {
                elapsedRayTime += Time.deltaTime;

                if(elapsedRayTime > rayCoolDown)
                {
                    elapsedRayTime = 0f;
                    rayReady = true;
                }
            }
        }
    }
    
}
