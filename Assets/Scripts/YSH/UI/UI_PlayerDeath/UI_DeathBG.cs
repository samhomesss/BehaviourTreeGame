using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_DeathBG : MonoBehaviour
{
    Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        Debug.Log("버튼 눌림");
        StartCoroutine(GoBackScene());
    }

    IEnumerator GoBackScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }

   
}
