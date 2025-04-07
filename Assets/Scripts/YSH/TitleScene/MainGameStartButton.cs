using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameStartButton : MonoBehaviour
{
    Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        if (_button == null)
        {
            return;
        }
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    
}
