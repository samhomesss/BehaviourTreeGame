using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    float _gravityStrength;
    float _gravityScale;
    float _normalGravityScale;
    private float _jumpForce;

    float _canJumpTime; // ���� Ÿ�� ������ CanJump

    [Header("���� ���� �߷� ���� ����")]
    [SerializeField] float JumpHight = 10f; // ���� ���� // �߷� ���� �� ��� ��
    [SerializeField] float JumpTimeToApex = 0.6f; // �ְ����� �����ϴ�
    [SerializeField] float JumpBufferTime = 0.05f; // ���� ��ư�� ������ �� ������ �� ���� Ÿ��
    [SerializeField] float FallGravityMultiplier = 1.5f; // �������� ��� �� �߷� �����
    [SerializeField] float MaxFallSpeed = 25f; // ������ �� �ִ� �ӵ� 

    // ������ ���������� Ŭ������ ���� �����ؾ���. �ϴ� ��ɱ�������.
    private void Start()
    {
        Managers.InputManager.OnJumpEvent += Jump; // JumpAction ���� 

        _gravityStrength = -(2 * JumpHight) / (JumpTimeToApex * JumpTimeToApex); // �߷� �� ��� 
        _gravityScale = _gravityStrength / Physics2D.gravity.y; //�߷� ���� ���
        _normalGravityScale = _gravityScale; // ���� �߷����� ������ ��� 
        _jumpForce = Mathf.Abs(_gravityStrength) * JumpTimeToApex; //���� �� �߷� ������ �ְ��� ���� ���� ����

        Managers.InputManager.OnJumpCutEvent += JumpCutGravity; // ���� ������ ��¦ �ٵ��� ����� �� 
    }

    private void Update()
    {
            _canJumpTime -= Time.deltaTime; // ���� ���� Ÿ���� ����ؼ� ���ߴ°� 

            if (_canJumpTime > 0 && PlayerStateManager.IsGrounded) // ���� ���� Ÿ������ ���ؼ� ���� �꿴���� ������ �� 
            {
                Jump();
            }

            if (PlayerStateManager.PlayerRigid.linearVelocity.y > MaxFallSpeed) // �÷��̾ �����Ҷ� �ְ� �ӷ��� MaxFallSpeed�� ���� �ʵ��� ����
            {
                PlayerStateManager.PlayerRigid.linearVelocity = new Vector2(PlayerStateManager.PlayerRigid.linearVelocity.x, MaxFallSpeed);
            }

            // ������ ���� �ߴ�.
            if (PlayerStateManager.PlayerRigid.linearVelocity.y < 0)
            {
                // �߷� ���� ���� �ȵǰ� �� 
                PlayerStateManager.PlayerRigid.gravityScale = _gravityScale * FallGravityMultiplier;
            }
            else // �Ϲ����� �߷�
                PlayerStateManager.PlayerRigid.gravityScale = _gravityScale;
        }
        

    // ���� ���� �� 
    private void Jump()
    {
        _canJumpTime = JumpBufferTime; // ���� ���� Ÿ���� ���� ���� �ְ� 

        // �����ϰ� �ְų� ����ϰ� �ִٸ� �������� ����
        if (PlayerStateManager.IsJumping || PlayerStateManager.IsDashing) // ���� IsJumping�� True��� �ٷ� Return
        {
            return;
        }

        if (PlayerStateManager.LastGroundTimer > 0 ) // ���� ���� ���� ��� �ϴ°� �̲��� ���� �ڿ��� Ÿ�� ����ؼ� ���� �Ҽ� �ֵ��� ���
        {
            if (!PlayerStateManager.IsGrounded) // ���� ���� ������ ���� ���� ���� ���� ���ֱ� ���ؼ� 1.5f �� ������
            {
                PlayerStateManager.PlayerRigid.AddForce(Vector2.up * _jumpForce * 1.5f, ForceMode2D.Impulse);
            }
            else // ���� �������� ���� ������ 
                PlayerStateManager.PlayerRigid.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            StartCoroutine(WaitOneSecondCouroutine()); // 1�� ������� Jump = false�� �ǵ����� 
        }
        
    }

    // ���� �� 
    void JumpCutGravity()
    {
        _gravityScale = _gravityScale * FallGravityMultiplier; // �߷°� ��� 2�踦 ���ؼ� ������ �� ���ִ� �뵵 
        PlayerStateManager.PlayerRigid.AddForce(Vector2.down * _gravityScale, ForceMode2D.Impulse); // �ش� �߷��� �������� ���� ���ؼ� ���̸� ����
        _gravityScale = _normalGravityScale; // �ø� �߷� ���� ���� ��� ������
    }

    // IsJumping�� ture�� ���� 
    IEnumerator WaitOneSecondCouroutine()
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        PlayerStateManager.IsJumping = true;

    }

    private void OnDisable()
    {
        Managers.InputManager.OnJumpEvent -= Jump; // JumpAction ����
        Managers.InputManager.OnJumpCutEvent -= JumpCutGravity; // ���� ������ ��¦ �ٵ��� ����� �� 
    }
}