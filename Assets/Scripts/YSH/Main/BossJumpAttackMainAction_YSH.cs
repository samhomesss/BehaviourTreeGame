using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossJumpAttack_Main", story: "[Self] is [CurrentState]", category: "Action", id: "095dacc6380fb8ec30a388ebb21c3658")]
public partial class BossJumpAttackMainAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;

    const float JumpHeight = 1.5f; // 점프 높이 
    const float JumpSpeed = 0.05f; // 올라가는 속도
    const float FallSpeed = 0.07f;  // 떨어지는 속도 
    const float JumpDuration = 5f; // Duration
    const float FallDuration = 3f; // Duration

    Vector2 _jumpStartTargetPos; // 점프 시작할때 플레이어 머리 위로 이동할 변수 
    Vector2 _jumpAttackTarget; // 떨어지는 지점 
    float _timer; // 시간 으로 떨어지는거 계산
    float _bossOriginalPosY = -3.5f; // 보스를 원래대로 돌리기 위함
    bool _isFalling; // 떨어지는 중
    PlayerStateManager player; // 플레이어 참조 


    protected override Status OnStart()
    {
        player = GameObject.FindAnyObjectByType<PlayerStateManager>();
        if (player == null) return Status.Failure; // 플레이어 없으면 바로 실패 반환 

        _jumpStartTargetPos = new Vector2(player.transform.position.x, JumpHeight); // 플레이어 위로 가기 위함 
        _timer = 0f; // 시간 초기화 
        _isFalling = false; // 아직 안떨어짐 

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _jumpStartTargetPos = new Vector2(player.transform.position.x, JumpHeight); // 포지션을 계속 변경 하기 위함

        if (!_isFalling) // 올라가는 상태
        {
            UpdateJumpPhase();
        }
        else // 내려가는 상태 
        {
            return UpdateFallPhase();
        }

        return Status.Running;
    }

    // 올라갈 때 
    private void UpdateJumpPhase()
    {
        _timer += Time.fixedDeltaTime; // 시간 늘려주고 

        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, _jumpStartTargetPos, JumpSpeed); // 올라가는 시간 으로 Lerp

        if (_timer >= JumpDuration) // 떨어질때 되었다 싶으면
        {
            _isFalling = true; // 떨어지는거 만들어 주고 
            _timer = 0f; // Timer = 0 으로 만들어주고 

            _jumpAttackTarget = new Vector2(_jumpStartTargetPos.x, _bossOriginalPosY); //플레이어 위치 와 보스의 원래 Y 값으로 Attack Target 설정
        }
    }
    // 떨어질 때 
    private Status UpdateFallPhase()
    {

        _timer += Time.fixedDeltaTime; // 시간 늘리고 

        Vector2 fallTarget = new Vector2(_jumpAttackTarget.x, _bossOriginalPosY); // 원래 위치를 받아서 떨어질때 타겟으로 사용 
        //Vector2 fallTarget = new Vector2(_jumpStartTargetPos.x, _bossOriginalPosY); // 원래 위치를 받아서 떨어질때 타겟으로 사용 
        float fallProgress = Mathf.Clamp01(_timer / FallDuration); 

        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, fallTarget, FallSpeed);

        if (fallProgress >= 1f) // 떨어지다 시간 못맞추면 
        {
            Self.Value.transform.position = fallTarget; // 정확히 목표 위치로 설정
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        _timer = 0f;
        _isFalling = false;
    }
}


