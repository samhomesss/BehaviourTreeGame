using System.Collections;
using UnityEngine;

public class PlayerDamagedEffect : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Color damagedColor = new Color(1, 0.6f, 0.6f, 1);

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += Die;
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += _ => DamagedEffect();
    }

    private void Die(int playerHp)
    {
        if (playerHp <= 0)
        {
            // todo : 입력정지 코드
            Managers.InputManager.SetPlayerMoveable(false);
            PlayerStateManager.IsDeath = true;
            _animator.Play("DEATH");
        }
    }

    private void DamagedEffect()
    {
        StartCoroutine(DamagedEffectCoroutine());
    }

    IEnumerator DamagedEffectCoroutine()
    {
        _spriteRenderer.color = damagedColor;
        float time = 0f;
        while (time < 0.3f)
        {
            float t = time / 0.3f;
            float gb = Mathf.Lerp(150f / 255f, 1f, t);
            _spriteRenderer.color = new Color(1, gb, gb, 1);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
