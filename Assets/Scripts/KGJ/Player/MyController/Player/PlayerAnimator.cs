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
        if (PlayerStateManager.IsDashing)
        {
            _animator.Play("DASH");
        }
        else if (PlayerStateManager.IsJumping)
        {
            _animator.Play("JUMP");
        }
        else if (!PlayerStateManager.IsJumping && Managers.InputManager.IsMove)
        {
            _animator.Play("RUN");
        }
        else if (!PlayerStateManager.IsJumping && !Managers.InputManager.IsMove)
        {
            _animator.Play("IDLE");
        }
        
    }
}
