using UnityEngine;

public class Aazz0200_MoveFwd1 : MonoBehaviour
{
    public float speed;
    public float innacuracy;//발사위치 높이 ~~



    // Start is called before the first frame update
    void Start()
    {

        transform.Rotate(transform.forward, Random.Range(-innacuracy, innacuracy)); //랜덤 Y축 회전     


    }
    public void set(float v) { speed *= -1; }
    // Update is called once per frame
    void Update()
    {
        //전진
        transform.position += transform.up * Time.deltaTime * speed;

    }
}
