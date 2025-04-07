using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���� �÷��̾ �� �ִ� ��� State�� ���� ���ְ� 
/// ���� �÷��̾��� RigidBody , Speed�� �÷��̾�� �����ϴ� ���� �ƴ� ���������� ���� ����� �͵��� ���� 
/// ��ǻ� �� ����� ���信 ������ ���� �� ������, �̹��� �����ϴ� ���鿡�� �ۼ� �غ��� �� 
/// �޸𸮿� �Ҵ� ���Ѽ� ��� ���� ������ �� �ֵ��� �ϴ� �ڵ� 
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
    float _playerSpeed = 14.5f;//9.5f; // �÷��̾� Max Speed �� ������.
    float _playerJumpPower = 30f;

    Transform _groundCheckPoint; // ���� ������ CheckPoint ��ġ 
    Vector2 _groundCheckSize = new Vector2(0.49f, 0.02f); // ������ �簢���� Vector ��
    LayerMask _groundLayer = 1 << 6; // Layer�� ��ġ 

    float _lastGroundedTimer;
    bool _isJumping = false;
    bool _isGrounded = true;
    const float JumpCoyoteTime = 0.1f;

    // ��� ���� ������
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
