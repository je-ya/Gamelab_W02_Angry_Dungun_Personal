using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D Customcursor;
    void Start()
    {
        Vector2 hotspot = new Vector2(70, 50);
        Cursor.SetCursor(Customcursor, hotspot, CursorMode.Auto);
    }
}
