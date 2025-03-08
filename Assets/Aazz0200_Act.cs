using System.Collections;
using UnityEngine;

public class Aazz0200_Act : MonoBehaviour
{
    [Header("Weapon Prefabs")]
    public GameObject defaultBulletPref; // �⺻ �Ѿ� 
    public GameObject iceRelicBulletPref; // ���� ���� �������� ������ ������ ����� �Ѿ�
    public GameObject wormRelicShieldObj; // ���� ���� �������� ������ ���� �� ������ ����. Prefab�� �ƴ϶� ���� ������Ʈ�� enable ����.
    public GameObject lightRelicRayPref; // �� ���� �������� ������ ���� �� ������ Ư�� ����.

    public float col_str; // �Ⱦ����� 
    public float col_max; // ����ӵ�
    float col_ing; // ���� �ӵ� �ð�?

    [Header("Relic Prefabs")]
    public Relic _iceRelic; // ���� ���� ������ 
    public Relic _wormRelic; // ���� ���� ������ 
    public Relic _lightRelic; // �� ���� ������

    [Header("WormShield References")]
    public float wormShieldCoolDown = 5f;// �Ѿ� ������ 5�ʵڿ� �ٽ� �����°� 
    [SerializeField] private float elapsedWormShieldTime = 0;
    [SerializeField] private bool isWormShieldEnabled = false;

    [Header("Ray References")]
    public float rayCoolDown = 3f;
    [SerializeField] private float elapsedRayTime = 0f;
    [SerializeField] private bool rayReady = false;
    

    float _nomarlCol = 0.1f; // ������ �Ծ����� �ٽ� ���ƿ� ���� �ӵ� 

    public float Col_Max
    {

        get { return col_max; }
        set
        {
            col_max = value;
            // �ڷ�ƾ ����ؼ� ���ʵڿ� �ٽ� ���� ���� �ɵ�?
            StartCoroutine(CoolDownShootSpeed());
        }
    }


    // Update is called once per frame
    void Update()
    {
        //================== ������ ==================
        if (col_ing < col_max)
        {
            col_ing += Time.deltaTime;

            //������
            if (col_ing >= col_max)
                col_ing = col_max;
        }
        //================== ������ ==================

        //���� ���� ����.
        ManageWormShield();

        //������ ����
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


    //���� ���� ����
    public void ManageWormShield()
    {
        if(wormRelicShieldObj == null)
        {
            Debug.LogWarning("WormRelicObj ����. Player(Core) ���� GameObject������.");
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

    public void disableWormShieldState() //���� ���� ����.
    {
        wormRelicShieldObj.SetActive(false); //���� ���� �ı�.

        isWormShieldEnabled = false;

    }

    //���̿�
    private void ManageRay()
    {
        if (_lightRelic == null)
        {
            return;
        }
        if(!_lightRelic.RelicActive) //�� ���� ����
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
