using UnityEngine;

public class KHW_Lazor : MonoBehaviour
{
    [SerializeField] GameObject razor; //경고 점선.
    [SerializeField] float razorSize = 3f;

    void Start()
    {
        Invoke("CreateRazor", 1f);
    }
    void Update()
    {

    }

    void CreateRazor()
    {
        razor.transform.localScale = new Vector3(razor.transform.localScale.x, razorSize, razor.transform.localScale.y);

        //SpriteRenderer spriteRenderer = razor.GetComponent<SpriteRenderer>();
        //BoxCollider2D boxCollider = razor.GetComponent<BoxCollider2D>();

        //if(boxCollider && spriteRenderer)
        //{
        //    Vector2 spriteSize = spriteRenderer.bounds.size;
        //    boxCollider.size = new Vector2(spriteSize.x / razor.transform.localScale.x, spriteSize.y / razor.transform.localScale.y);
        //}

        Invoke("DisableRazor", 0.5f);
    }

    void DisableRazor()
    {
        Destroy(gameObject);
    }
}
