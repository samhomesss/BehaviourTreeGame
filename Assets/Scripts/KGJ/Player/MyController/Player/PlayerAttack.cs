using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float _bufferTime = 0.3f;
    float _bufferTimer = 0f;
    bool _attackBuffered = false;

    void Start()
    {
        Managers.InputManager.OnAttackEvent += Attack;   
    }

    void Update()
    {
        if (_bufferTimer > 0)
        {
            _bufferTimer -= Time.deltaTime;
            if (_bufferTimer <= 0)
            {
                _attackBuffered = false; // ���� Ÿ���� ������ �Է� ��ȿȭ
            }
        }
    }

    void Attack()
    {
        PlayerStateManager.IsAttacking = true;
        PlayerStateManager.IsAttackCooltime = false;
    }

    public void EndAttack()
    {
        PlayerStateManager.IsAttacking = false;
        PlayerStateManager.IsAttackCooltime = false;
    }

    void OnDisable()
    {
        Managers.InputManager.OnAttackEvent -= Attack;
    }
}
