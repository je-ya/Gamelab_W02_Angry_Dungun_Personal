using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D Customcursor;
    void Start()
    {
        Cursor.SetCursor(Customcursor, Vector2.zero, CursorMode.Auto);
    }
}
