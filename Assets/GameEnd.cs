using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    public void EndGame()
    {
        SceneManager.LoadScene("ThankyouScene");
    }
}
