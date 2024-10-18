
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class CameraBounds : MonoBehaviour
{
    // Static variable to store the screen bounds
    public static Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        // Get the screen width and height in pixels
        screenBounds = new Vector2(Screen.width, Screen.height);
        
        // Convert the screen bounds from pixels to world units
        screenBounds = Camera.main.ScreenToWorldPoint(screenBounds);
        
        // Log the screen bounds for debugging purposes
        Debug.Log("Screen bounds" + screenBounds);
    }
}
