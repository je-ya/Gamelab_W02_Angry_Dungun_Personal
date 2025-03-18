using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Team { Plyaer, Enemy }


public class Aazz0200_Player : MonoBehaviour
{
    float _normalSpeed = 10f; // 원래 플레이어 속도 
    public float PlayerSpeed
    {
        get { return move_speed; }
        set
        {
            move_speed = value;
            // 코루틴 설정 해서 원래 값으로 되돌리면 될듯?
            StartCoroutine(DownPlayerSpeed());
        }
    }

    public float move_speed = 5;
    public Aazz0200_Act[] acts;
    public Aazz0200_Act act_now;
    [SerializeField]
    bool canShoot = true;
    [SerializeField]
    bool canMove = true;

    public bool CanShoot
    {
        get { return canShoot; }
        set { canShoot = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        acts = GetComponentsInChildren<Aazz0200_Act>();
        act_now = acts[0];
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //이동

        if (canMove == true)
        {
            float X = Input.GetAxisRaw("Horizontal");
            float Y = Input.GetAxisRaw("Vertical");
            transform.Translate(new Vector2(X, Y).normalized * Time.deltaTime * move_speed);

        }


        if (canShoot == true && Input.GetMouseButton(0))
        {
            var to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            to.z = 0;

            act_now.Start_Act(gameObject, transform.position, to);

        }

        if (Input.GetKeyDown(KeyCode.R))
            GetComponent<Aazz0200_Life>().now += 100;


    }

    IEnumerator DownPlayerSpeed()
    {
        yield return new WaitForSeconds(5f);
        move_speed = _normalSpeed;
    }

    public void GameEnd()
    {
        Time.timeScale = 0;


    }


    //다시시작
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}





/*
 
        if (Input.GetMouseButton(1))
        {
            var to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            to.z = 0;

            acts[1].Start_Act(gameObject, transform.position, to);

        }


        ////무기교체 
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Debug.Log(SceneManager.GetActiveScene().buildIndex);
        //    if (SceneManager.GetActiveScene().buildIndex != 0)
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetAllScenes().Length - 2)
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
 
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //Vector3 tp = transform.position;
        //
        //transform.position += new Vector3(h * move_speed * Time.deltaTime, 0);
        //transform.position += new Vector3(0, v * move_speed * Time.deltaTime, 0);
*/