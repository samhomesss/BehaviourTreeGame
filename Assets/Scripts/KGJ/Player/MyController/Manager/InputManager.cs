using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public Vector2 MoveVec => _moveVector;
    public bool IsMove => _isMove;
    public bool IsJumpPress => _isJumpPress;
    public bool IsJumpCut
    {
        get
        {
            return _isJumpCut;
        }
        set
        {
            _isJumpCut = value;
        }
    }

    InputSystem_Actions _myPlayerInputSystem; // 나의 InputSystem 가져오기 
    PlayerInput _playerInput;
    InputAction _moveAction; // 움직임 Action
    InputAction _jumpAction; // 점프 Action
    InputAction _dashAction; // 대시 Action
    InputAction _attackAction;

    Vector2 _moveVector; // 움직인 값을 받아 오기 위한 벡터 
    bool _isMove; // 현재 움직이는가?
    bool _isJumpCut;
    bool _isJumpPress; // 점프 뛰었을때 

    public Action OnJumpEvent; // Jump 되었을때 Action 실행 
    public Action OnJumpCutEvent; // OnJumpCut Action
    public Action OnDashEvent; // Dash 키 인풋 시 Action 실행
    public Action OnAttackEvent;

    public void Init()
    {
        _myPlayerInputSystem = new InputSystem_Actions(); // 내 InputSystem 가져옴
        _playerInput = GameObject.FindAnyObjectByType<PlayerInput>();
        _playerInput.neverAutoSwitchControlSchemes = true;
        _moveAction = _myPlayerInputSystem.MyPlayer.Move; // 내 InputSystem에서 해당 Action이 어떤 액션 을 참조하는지 
        _jumpAction = _myPlayerInputSystem.MyPlayer.Jump;
        _dashAction = _myPlayerInputSystem.MyPlayer.Dash;
        _attackAction = _myPlayerInputSystem.MyPlayer.Attack;
        
        _myPlayerInputSystem.MyPlayer.Enable();
        _moveAction.Enable(); // 연결 
        _jumpAction.Enable();
        _dashAction.Enable();
        _attackAction.Enable();

        _moveAction.performed += OnMove; // 어떤 함수 실행일지 연결 
        _moveAction.canceled += OnMove;

        _jumpAction.started += OnJump;
        _jumpAction.canceled += OnJump;

        _dashAction.performed += OnDash;

        _attackAction.performed += OnAttack;

        _isJumpCut = false;

    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            _moveVector = moveInput;
            if (_moveVector.x > 0.01f || _moveVector.x < -0.01f)
                _isMove = true;
            else if (_moveVector.x < 0.01f || _moveVector.x > -0.01f)
                _isMove = false;
        }

        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveVector = Vector2.zero;
            _isMove = false;
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnJumpEvent?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if (PlayerStateManager.IsJumping && PlayerStateManager.PlayerRigid.linearVelocity.y > 0)
            {
                _isJumpCut = true;
                OnJumpCutEvent?.Invoke();
            }
        }
    }

    void OnDash(InputAction.CallbackContext context)
    {
        OnDashEvent?.Invoke();
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        OnAttackEvent?.Invoke();
    }

    /// <summary>
    /// 플레이어가 죽었을때 실행 시킬 Clear
    /// </summary>
    public void Clear()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
        _dashAction.Disable();
        _attackAction.Disable();

        _myPlayerInputSystem.MyPlayer.Disable();
        OnJumpEvent = null;
    }
}
