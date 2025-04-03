using UnityEngine;
using UnityEngine.InputSystem;

public class Managers : MonoBehaviour
{
    public static Managers Instance => _instance;
    static Managers _instance;

    public static InputManager InputManager => Instance._inputManager;
    InputManager _inputManager = new InputManager();


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

       // DontDestroyOnLoad(this);
        // �ٸ� �Ŵ����� Init ���ָ� �� 
        InputManager.Init();
    }

    private void OnDisable()
    {
        InputManager.Clear();
    }
}
