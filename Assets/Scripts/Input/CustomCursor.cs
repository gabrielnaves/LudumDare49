using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Cursor.visible = IsCursorOutsideGameWindow();
        transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    bool IsCursorOutsideGameWindow()
    {
        return Input.mousePosition.x < 0 ||
               Input.mousePosition.y < 0 ||
               (Input.mousePosition.x / Screen.width > 1) ||
               (Input.mousePosition.y / Screen.height > 1);
    }
}
