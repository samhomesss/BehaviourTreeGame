using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 현재 플레이어에 들어가 있는 모든 State를 정의 해주고 
/// 현재 플레이어의 RigidBody , Speed등 플레이어에서 조작하는 것이 아닌 공통적으로 많이 사용할 것들을 정의 
/// 사실상 이 방법이 정답에 가깝지 않을 수 있지만, 이번엔 도전하는 측면에서 작성 해보는 중 
/// 메모리에 할당 시켜서 어디서 든지 가져올 수 있도록 하는 코드 
/// </summary>
public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance => _instance;
    static PlayerStateManager _instance;
    public static Rigidbody2D PlayerRigid => Instance._playerRigid;
    public static float PlayerSpeed => Instance._playerSpeed;
    public static float PlayerJumpPower => Instance._playerJumpPower;
    public static float LastGroundTimer => Instance._lastGroundedTimer;
    public static bool IsGrounded
    {
        get
        {
            return Instance._isGrounded;
        }
        set
        {
            Instance._isGrounded = value;
        }
    }
    public static bool IsJumping
    {
        get
        {
            return Instance._isJumping;
        }
        set
        {
            Instance._isJumping = value;
        }
    }
    public static bool IsDashing
    {
        get 
        {
            return Instance._isDashing;
        } 
        set 
        { 
            Instance._isDashing = value; 
        }
    }

    public static bool FilpX
    {
        get
        {
            return Instance._filpX;
        }
        set
        {
            Instance._filpX = value;
        }
    }

    public static bool IsAttacking
    {
        get { return Instance._isAttacking; }
        set { Instance._isAttacking = value; }
    }


    public static int Combo
    {
        get { return Instance._combo; }
        set { Instance._combo = value; }
    }

    public static bool IsAttackCooltime
    {
        get { return Instance._isAttackCooltime; }
        set { Instance._isAttackCooltime = value; }
    }

    public static bool IsDeath
    {
        get { return Instance._isDeath; }
        set { Instance._isDeath = value; }
    }

    Rigidbody2D _playerRigid;
    float _playerSpeed = 14.5f;//9.5f; // 플레이어 Max Speed 에 가깝다.
    float _playerJumpPower = 30f;

    Transform _groundCheckPoint; // 땅을 판정할 CheckPoint 위치 
    Vector2 _groundCheckSize = new Vector2(0.49f, 0.02f); // 땅판정 사각형의 Vector 값
    LayerMask _groundLayer = 1 << 6; // Layer의 수치 

    float _lastGroundedTimer;
    bool _isJumping = false;
    bool _isGrounded = true;
    const float JumpCoyoteTime = 0.1f;

    // 대시 관련 변수들
    bool _isDashing = false;

    bool _filpX = false;

    bool _isAttacking = false;
    int _combo = 0;
    bool _isAttackCooltime = false;

    bool _isDeath = false;

    private void Awake()
    {
        _instance = this;
        Init();
    }

    private void Update()
    {
       _lastGroundedTimer -= Time.deltaTime;
        IsGrounded = isGrounded();
    }

    void Init()
    {
        _groundCheckPoint = GetComponentInChildren<PlayerGroundCheckPos>().transform;
        _playerRigid = GetComponent<Rigidbody2D>();
    }

    bool isGrounded()
    {
        if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
        {
            _lastGroundedTimer = JumpCoyoteTime;
            _isJumping = false;
            _isGrounded = true;
            StartCoroutine(GamePadMotor());
        }
        else
        {
            _isGrounded = false;
        }
        return _isGrounded;
    }

    IEnumerator GamePadMotor()
    {
        Gamepad.current?.SetMotorSpeeds(0.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Gamepad.current?.SetMotorSpeeds(0f, 0f);
    }
}
