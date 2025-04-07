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

    private void Update()
    {
        if (Input.anyKeyDown && _button.enabled)
        {
            Debug.Log("����");
            ButtonClick();
        }
    }


    void ButtonClick()
    {
        Debug.Log("��ư ����");
        StartCoroutine(GoBackScene());
    }

    IEnumerator GoBackScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }

   
}
