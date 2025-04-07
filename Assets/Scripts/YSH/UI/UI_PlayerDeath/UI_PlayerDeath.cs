using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerDeath : MonoBehaviour
{
    Canvas _canvas;
    Animator _deathAni;
    Button _button;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _deathAni = GetComponent<Animator>();
        _button = GetComponentInChildren<Button>();
        _canvas.enabled = false;
        _button.enabled = false;
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += StartAni;
    }

    void StartAni(int playerHp)
    {
        if (playerHp == 0)
        {
            //사운드
            SoundManager.Instance.PlayDeathBellSound();
            SoundManager.Instance.PlayCrowSound();
            _button.enabled = true;
            _canvas.enabled = true;
            _deathAni.Play("DeathUIStart");
        }
    }
}
