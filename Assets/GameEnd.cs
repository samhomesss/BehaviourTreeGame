using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public void EndGame()
    {
        SceneManager.LoadScene("ThankyouScene");
    }
}
