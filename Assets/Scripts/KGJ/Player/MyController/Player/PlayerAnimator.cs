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
        
        if (!PlayerStateManager.IsAttackCooltime && PlayerStateManager.IsAttacking)
        {
            _animator.Play("ATK1");
            
            PlayerStateManager.IsAttackCooltime = true;
        }
        else if (!PlayerStateManager.IsAttacking && PlayerStateManager.IsDashing)
        {
            _animator.Play("DASH");
        }
        else if (!PlayerStateManager.IsAttacking &&  PlayerStateManager.IsJumping)
        {
            _animator.Play("JUMP");
        }
        else if (!PlayerStateManager.IsAttacking &&  !PlayerStateManager.IsJumping && Managers.InputManager.IsMove)
        {
            _animator.Play("RUN");
        }
        else if (!PlayerStateManager.IsAttacking &&  !PlayerStateManager.IsJumping && !Managers.InputManager.IsMove)
        {
            _animator.Play("IDLE");
        }
        
    }
}
