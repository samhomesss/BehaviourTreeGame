using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform _groundCheckPoint; // 땅을 판정할 CheckPoint 위치 
    Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f); // 땅판정 사각형의 Vector 값
    LayerMask _groundLayer = 1 << 6; // Layer의 수치 

    bool _isFacingRight = true; // Flip X 기능을 만든 거 
    float _lastOnGroundTime; // 땅에서 판정이 되었을때 

    float _runAceelAmount = 9.5f; // 달리기 가속도 계산 수치 
    float _runDeccelAmount = 9.5f; // 달리기 감속도 계산 수치 

    float _accelInAir = 1f; // 공중에 있을때 가속도 계산 수치 
    float _deccelInAir = 1f; // 공중에 있을때 감속도 계산 수치 

    bool _doConserveMomentum = true; // 플레이어의 최대 속도를 유지 시킬건지 아닌지 

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
        // 기본적인 플레이어의 이동 속도를 현재 받아오는 MoveVec 값 에 플레이어가 가지는 RunMaxSpeed를 곱해서 구한다 

        float playerSpeed = Managers.InputManager.MoveVec.x * PlayerStateManager.PlayerSpeed;
        // 가속도를 구할 거
        float accelRate;

        if (_lastOnGroundTime > 0) // 바닥에 있다가 공중으로 바뀐 상태를 의미함 -> 바닥에서 미끄러져 내려온 상황
            accelRate = (Mathf.Abs(playerSpeed) > 0.01f) ? _runAceelAmount : _runDeccelAmount;
        else
            accelRate = (Mathf.Abs(playerSpeed) > 0.01f) ? _runAceelAmount * _accelInAir : _runDeccelAmount * _deccelInAir;


        // 보존 수치가 켜져있는지 && 플레이어가 이미 목표 속도보다 빠르게 움직이고 있는가? && 속도와 목표 속도가 같은 방향으로 움직이고 있는지 
        // 작은 값인 (0.01f) 이하의 값을 무시함으로서 미세한 속도 변화로 인해서 조건이 참이 되는것을 방지함 
        // 플레이어가 공중에 떠 있을 경우에 위의 조건들을 다 만족 한다면 AccelRate를 0으로 변환 한다.
        if (_doConserveMomentum && Mathf.Abs(PlayerStateManager.PlayerRigid.linearVelocity.x) > Mathf.Abs(playerSpeed)
            && Mathf.Sign(PlayerStateManager.PlayerRigid.linearVelocity.x) == Mathf.Sign(playerSpeed) && Mathf.Abs(playerSpeed) > 0.01f 
            && _lastOnGroundTime < 0 )
        {
            accelRate = 0;
        }


        // 플레이어의 이동 속도를 고려한 이동 방향 + 스피드를 구하고 
        float speedDif = playerSpeed - PlayerStateManager.PlayerRigid.linearVelocity.x;
        float moveMent = speedDif * accelRate; // 해당 움직임이 가속인지 감속인지를 계산하고 
        
        PlayerStateManager.PlayerRigid.AddForce(moveMent * Vector2.right, ForceMode2D.Force); // 해당 방향으로 힘을 줘서 가속하거나 감속함

        #region 원래 코드
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
    /// 플레이어가 땅에 있는지를 보기위함 
    /// </summary>
    void IsGrounded()
    {
        if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
            _lastOnGroundTime = 0.1f;

    }
}