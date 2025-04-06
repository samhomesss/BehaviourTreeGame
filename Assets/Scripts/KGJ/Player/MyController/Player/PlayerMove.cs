using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform _groundCheckPoint; // ���� ������ CheckPoint ��ġ 
    Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f); // ������ �簢���� Vector ��
    LayerMask _groundLayer = 1 << 6; // Layer�� ��ġ 

    bool _isFacingRight = true; // Flip X ����� ���� �� 
    float _lastOnGroundTime; // ������ ������ �Ǿ����� 

    float _runAceelAmount = 9.5f; // �޸��� ���ӵ� ��� ��ġ 
    float _runDeccelAmount = 9.5f; // �޸��� ���ӵ� ��� ��ġ 

    float _accelInAir = 1f; // ���߿� ������ ���ӵ� ��� ��ġ 
    float _deccelInAir = 1f; // ���߿� ������ ���ӵ� ��� ��ġ 

    bool _doConserveMomentum = true; // �÷��̾��� �ִ� �ӵ��� ���� ��ų���� �ƴ��� 

    SpriteRenderer _playerSpriteRenderer;

    public bool IsFacingRight => _isFacingRight;

    private void Start()
    {
        _groundCheckPoint = GetComponentInChildren<PlayerGroundCheckPos>().transform;
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _lastOnGroundTime -= Time.deltaTime;

        /*if (Managers.InputManager.MoveVec.x != 0)
            CheckDirectionToFace(Managers.InputManager.MoveVec.x > 0);
        */

        
        IsGrounded();
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    void Move()
    {
        // �⺻���� �÷��̾��� �̵� �ӵ��� ���� �޾ƿ��� MoveVec �� �� �÷��̾ ������ RunMaxSpeed�� ���ؼ� ���Ѵ� 

        float playerSpeed = Managers.InputManager.MoveVec.x * PlayerStateManager.PlayerSpeed;
        // ���ӵ��� ���� ��
        float accelRate;

        if (_lastOnGroundTime > 0) // �ٴڿ� �ִٰ� �������� �ٲ� ���¸� �ǹ��� -> �ٴڿ��� �̲����� ������ ��Ȳ
            accelRate = (Mathf.Abs(playerSpeed) > 0.01f) ? _runAceelAmount : _runDeccelAmount;
        else
            accelRate = (Mathf.Abs(playerSpeed) > 0.01f) ? _runAceelAmount * _accelInAir : _runDeccelAmount * _deccelInAir;


        // ���� ��ġ�� �����ִ��� && �÷��̾ �̹� ��ǥ �ӵ����� ������ �����̰� �ִ°�? && �ӵ��� ��ǥ �ӵ��� ���� �������� �����̰� �ִ��� 
        // ���� ���� (0.01f) ������ ���� ���������μ� �̼��� �ӵ� ��ȭ�� ���ؼ� ������ ���� �Ǵ°��� ������ 
        // �÷��̾ ���߿� �� ���� ��쿡 ���� ���ǵ��� �� ���� �Ѵٸ� AccelRate�� 0���� ��ȯ �Ѵ�.
        if (_doConserveMomentum && Mathf.Abs(PlayerStateManager.PlayerRigid.linearVelocity.x) > Mathf.Abs(playerSpeed)
            && Mathf.Sign(PlayerStateManager.PlayerRigid.linearVelocity.x) == Mathf.Sign(playerSpeed) && Mathf.Abs(playerSpeed) > 0.01f 
            && _lastOnGroundTime < 0 )
        {
            accelRate = 0;
        }


        // �÷��̾��� �̵� �ӵ��� ����� �̵� ���� + ���ǵ带 ���ϰ� 
        float speedDif = playerSpeed - PlayerStateManager.PlayerRigid.linearVelocity.x;
        float moveMent = speedDif * accelRate; // �ش� �������� �������� ���������� ����ϰ� 
        
        PlayerStateManager.PlayerRigid.AddForce(moveMent * Vector2.right, ForceMode2D.Force); // �ش� �������� ���� �༭ �����ϰų� ������

        #region ���� �ڵ�
        //Vector2 playerMoveVelocity = Vector2.zero;

        //if (Managers.InputManager.IsMove)
        //{
        //    Vector2 moveDir = Managers.InputManager.MoveVec;
        //    playerMoveVelocity = moveDir * PlayerStateManager.PlayerSpeed;
        //}

        //PlayerStateManager.PlayerRigid.linearVelocity = new Vector2(playerMoveVelocity.x, PlayerStateManager.PlayerRigid.linearVelocity.y);
        #endregion
    }

    void Flip()
    {
        if (Managers.InputManager.MoveVec.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            PlayerStateManager.FilpX = true;
        }
        else if (Managers.InputManager.MoveVec.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            PlayerStateManager.FilpX = false;
        }
    }

    /// <summary>
    /// �÷��̾ ���� �ִ����� �������� 
    /// </summary>
    void IsGrounded()
    {
        if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
            _lastOnGroundTime = 0.1f;

    }
}
