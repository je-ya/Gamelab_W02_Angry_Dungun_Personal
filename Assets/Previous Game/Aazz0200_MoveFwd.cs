using UnityEngine;

public class Aazz0200_MoveFwd : MonoBehaviour
{
    public float speed;
    public float innacuracy;//�߻���ġ ���� ~~



    // Start is called before the first frame update
    void Start()
    {

        transform.Rotate(transform.forward, Random.Range(-innacuracy, innacuracy)); //���� Y�� ȸ��     


    }

    // Update is called once per frame
    void Update()
    {
        //����
        transform.position += transform.up * Time.deltaTime * speed;

    }
}
