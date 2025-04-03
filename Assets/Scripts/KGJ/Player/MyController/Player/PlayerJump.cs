using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    float _gravityStrength;
    float _gravityScale;
    float _normalGravityScale;
    private float _jumpForce;

    float _canJumpTime; // 버퍼 타임 가져올 CanJump

    [Header("점프 상태 중력 관리 변수")]
    [SerializeField] float JumpHight = 10f; // 점프 높이 // 중력 힘에 들어갈 상수 값
    [SerializeField] float JumpTimeToApex = 0.6f; // 최고점에 도달하는
    [SerializeField] float JumpBufferTime = 0.05f; // 점프 버튼을 눌렀을 때 반응이 될 버퍼 타임
    [SerializeField] float FallGravityMultiplier = 1.5f; // 떨어질때 계산 한 중력 상수값
    [SerializeField] float MaxFallSpeed = 25f; // 떨어질 때 최대 속도 

    // 원래는 점프데이터 클래스를 만들어서 관리해야함. 일단 기능구현부터.
    private void Start()
    {
        Managers.InputManager.OnJumpEvent += Jump; // JumpAction 실행 

        _gravityStrength = -(2 * JumpHight) / (JumpTimeToApex * JumpTimeToApex); // 중력 힘 계산 
        _gravityScale = _gravityStrength / Physics2D.gravity.y; //중력 정도 계산
        _normalGravityScale = _gravityScale; // 원래 중력으로 돌릴때 계산 
        _jumpForce = Mathf.Abs(_gravityStrength) * JumpTimeToApex; //점프 힘 중력 힘값과 최고점 도달 값을 곱함

        Managers.InputManager.OnJumpCutEvent += JumpCutGravity; // 점프 땟을때 살짝 뛰도록 만드는 거 
    }

    private void Update()
    {
            _canJumpTime -= Time.deltaTime; // 점프 버퍼 타임을 계속해서 낮추는거 

            if (_canJumpTime > 0 && PlayerStateManager.IsGrounded) // 점프 버퍼 타임으로 인해서 땅에 닿였을때 판정이 들어감 
            {
                Jump();
            }

            if (PlayerStateManager.PlayerRigid.linearVelocity.y > MaxFallSpeed) // 플레이어가 낙하할때 최고 속력이 MaxFallSpeed를 넘지 않도록 설정
            {
                PlayerStateManager.PlayerRigid.linearVelocity = new Vector2(PlayerStateManager.PlayerRigid.linearVelocity.x, MaxFallSpeed);
            }

            // 고점에 도달 했다.
            if (PlayerStateManager.PlayerRigid.linearVelocity.y < 0)
            {
                // 중력 값을 말도 안되게 줌 
                PlayerStateManager.PlayerRigid.gravityScale = _gravityScale * FallGravityMultiplier;
            }
            else // 일반적인 중력
                PlayerStateManager.PlayerRigid.gravityScale = _gravityScale;
        }
        

    // 점프 햇을 때 
    private void Jump()
    {
        _canJumpTime = JumpBufferTime; // 점프 버퍼 타임을 진행 시켜 주고 

        // 점프하고 있거나 대시하고 있다면 점프하지 않음
        if (PlayerStateManager.IsJumping || PlayerStateManager.IsDashing) // 만약 IsJumping이 True라면 바로 Return
        {
            return;
        }

        if (PlayerStateManager.LastGroundTimer > 0 ) // 땅에 떨어 질때 계산 하는거 미끄러 질때 코요테 타임 계산해서 점프 할수 있도록 계산
        {
            if (!PlayerStateManager.IsGrounded) // 땅에 있지 않을때 원래 점프 힘을 보장 해주기 위해서 1.5f 더 곱해줌
            {
                PlayerStateManager.PlayerRigid.AddForce(Vector2.up * _jumpForce * 1.5f, ForceMode2D.Impulse);
            }
            else // 땅에 있을때는 원래 점프로 
                PlayerStateManager.PlayerRigid.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            StartCoroutine(WaitOneSecondCouroutine()); // 1초 계산으로 Jump = false로 되돌려줌 
        }
        
    }

    // 땟을 때 
    void JumpCutGravity()
    {
        _gravityScale = _gravityScale * FallGravityMultiplier; // 중력값 계산 2배를 곱해서 빠르게 컷 해주는 용도 
        PlayerStateManager.PlayerRigid.AddForce(Vector2.down * _gravityScale, ForceMode2D.Impulse); // 해당 중력의 방향으로 힘을 곱해서 높이를 낮춤
        _gravityScale = _normalGravityScale; // 늘린 중력 값을 원래 대로 돌려줌
    }

    // IsJumping을 ture로 만듬 
    IEnumerator WaitOneSecondCouroutine()
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        PlayerStateManager.IsJumping = true;

    }

    private void OnDisable()
    {
        Managers.InputManager.OnJumpEvent -= Jump; // JumpAction 실행
        Managers.InputManager.OnJumpCutEvent -= JumpCutGravity; // 점프 땟을때 살짝 뛰도록 만드는 거 
    }
}