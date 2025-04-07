using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!PlayerStateManager.IsDeath && !PlayerStateManager.IsAttackCooltime && PlayerStateManager.IsAttacking)
        {
            if (PlayerStateManager.Combo == 0)
                _animator.Play("ATK1");
            else if (PlayerStateManager.Combo == 1)
                _animator.Play("ATK2");
            else if (PlayerStateManager.Combo == 2)
                _animator.Play("ATK3");

            PlayerStateManager.IsAttackCooltime = true;
        }
        else if (!PlayerStateManager.IsDeath && !PlayerStateManager.IsAttacking && PlayerStateManager.IsDashing)
        {
            _animator.Play("DASH");
        }
        else if (!PlayerStateManager.IsDeath && !PlayerStateManager.IsAttacking &&  PlayerStateManager.IsJumping)
        {
            _animator.Play("JUMP");
        }
        else if (!PlayerStateManager.IsDeath && !PlayerStateManager.IsAttacking &&  !PlayerStateManager.IsJumping && Managers.InputManager.IsMove)
        {
            _animator.Play("RUN");
        }
        else if (!PlayerStateManager.IsDeath && !PlayerStateManager.IsAttacking &&  !PlayerStateManager.IsJumping && !Managers.InputManager.IsMove)
        {
            _animator.Play("IDLE");
        }
        
    }
}
