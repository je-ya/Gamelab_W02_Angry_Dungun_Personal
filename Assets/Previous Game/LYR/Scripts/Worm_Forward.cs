using UnityEngine;

public class Worm_Forward : MonoBehaviour
{
    public float speed = 5f;
    public float zigzagAmount = 1f;
    public float zigzagSpeed = 2f;

    public bool isInside = true;
    public GameObject wormBoundary;
    public LayerMask CheckLayer;


    public enum PlayerState
    {
        Idle,
        Swing,
        Fast,
        Slow
    }

    public PlayerState currentState = PlayerState.Idle;

    public PlayerState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }



    void Update()
    {


        bool ischek = Physics2D.CircleCast(transform.position, 3, transform.up, 3, CheckLayer);
        switch (currentState)
        {


            case PlayerState.Idle:
                MoveFwd();
                break;

            case PlayerState.Swing:
                MoveFwd();
                float zigzagOffset = Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;
                Vector3 zigzagMovement = transform.right * zigzagOffset * Time.deltaTime * 10;
                transform.position += zigzagMovement;
                break;

            case PlayerState.Fast:
                float fastSpeed = speed * 5f;
                Vector3 fastMovement = transform.up * Time.deltaTime * fastSpeed;
                transform.position += fastMovement;


                if (ischek)
                    transform.Rotate(transform.forward, Random.RandomRange(-180, 180));//  direction *= -1;

                break;

            case PlayerState.Slow:
                float slowSpeed = speed * 0.5f;
                Vector3 slowMovement = transform.up * Time.deltaTime * slowSpeed;
                transform.position += slowMovement;

                if (ischek)
                    transform.Rotate(transform.forward, Random.RandomRange(-180, 180));//  direction *= -1;
                break;

            default:
                Debug.Log(".");
                break;
        }


    }


    void MoveFwd()
    {

        //Vector3 forwardMovement = transform.up * Time.deltaTime * speed;
        //transform.position += forwardMovement;
        //Debug.Log(transform.position.x );
        //Debug.Log(transform.position.y);
        //if (transform.position.x<-10&& transform.position.x>0&& transform.position.y<-8&& transform.position.y>-1)
        //{
        //    isInside = false;
        //    speed = -speed;
        //}

        Vector3 forwardMovement = transform.up * Time.deltaTime * speed;
        transform.position += forwardMovement;



        bool ischek = Physics2D.CircleCast(transform.position, 3, transform.up, 3, CheckLayer);

        if (ischek)
            transform.Rotate(transform.forward, Random.RandomRange(-180, 180));//  direction *= -1;

    }
}




/*
 
        //¾È
        if (Vector3.Distance(transform.parent.position, wormBoundary.transform.position) < 50)
        {
            Vector3 forwardMovement = transform.up * Time.deltaTime * speed;
            transform.position += forwardMovement;
        }//¹Û
        else
        {

            bool ischek = Physics2D.CircleCast(transform.position, 3, transform.up );

            if (ischek)
                transform.Rotate(transform.forward, Random.RandomRange(-180, 180));//  direction *= -1;
                    }
 
 
 
 
 
 */