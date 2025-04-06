using Unity.Behavior;
using UnityEngine;

public class PlayerDamagedEffect : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += Die;
    }

    private void Die(int playerHp)
    {
        if (playerHp <= 0)
        {
            // todo : �Է����� �ڵ�
            _animator.Play("DEATH");
        }
    }
}
