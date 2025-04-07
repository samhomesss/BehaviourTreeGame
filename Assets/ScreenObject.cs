using UnityEngine;

public class ScreenObject : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
}
