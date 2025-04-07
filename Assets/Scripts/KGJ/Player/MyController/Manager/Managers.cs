using UnityEngine;
using UnityEngine.InputSystem;

public class Managers : MonoBehaviour
{
    public static Managers Instance => _instance;
    static Managers _instance;

    public static InputManager InputManager => Instance._inputManager;
    InputManager _inputManager = new InputManager();

    public static CameraTargetManager CameraTargetManager => Instance._cameraTargetManager;
    CameraTargetManager _cameraTargetManager = new CameraTargetManager();

    private void Awake()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
                go.AddComponent<PlayerInput>();
            }
            _instance = go.GetComponent<Managers>();
        }
        else
            _instance = this;

        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        // DontDestroyOnLoad(this);
        // 다른 매니저들 Init 해주면 됨 
        InputManager.Init();
        CameraTargetManager.Init();
    }

    private void OnDisable()
    {
        InputManager.Clear();
    }
}
