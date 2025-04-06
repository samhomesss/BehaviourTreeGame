using UnityEngine;

public class UI_PlayerDeath : MonoBehaviour
{
    Canvas _canvas;
    Animator _deathAni;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _deathAni = GetComponent<Animator>();
        _canvas.enabled = false;
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += StartAni;
    }

    void StartAni(int playerHp)
    {
        if (playerHp == 0)
        {
            _canvas.enabled = true;
            _deathAni.Play("DeathUIStart");
        }
    }
}
