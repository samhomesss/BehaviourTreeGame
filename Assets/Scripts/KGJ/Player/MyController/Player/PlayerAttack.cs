using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    const float BUFFER_TIME = 0.2f;
    float _bufferTimer = 0f;
    bool _attackBuffered = false;

    void Start()
    {
        Managers.InputManager.OnAttackEvent += Attack;   
    }

    void Update()
    {
        if (_bufferTimer <= 0)
        {
            _attackBuffered = false;
            PlayerStateManager.Combo = 0;

            return;
        }
        _bufferTimer -= Time.deltaTime;

    }

    void Attack()
    {
        if (PlayerStateManager.IsAttackCooltime)
        {
            _attackBuffered = true;
            _bufferTimer = BUFFER_TIME;
            return;
        }

        PlayerStateManager.IsAttacking = true;
        PlayerStateManager.IsAttackCooltime = false;
    }

    public void EndAttack()
    {
        PlayerStateManager.IsAttacking = false;
        PlayerStateManager.IsAttackCooltime = false;

        if (_attackBuffered)
        {
            Attack();
        }

        if (PlayerStateManager.Combo == 2)
        {
            PlayerStateManager.Combo = 0;
        }
        else
        {
            PlayerStateManager.Combo++;
        }
    }

    void OnDisable()
    {
        Managers.InputManager.OnAttackEvent -= Attack;
    }
}
