using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BackJump_KDY", story: "[Self] BackJump to [CurrentDirection]", category: "Action", id: "907eee437fa56336bc3915bdd5f69d5f")]
public partial class BackJumpKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;

    // 점프 관련 설정
    [SerializeField] private float _jumpSpeed = 10.0f;   // 점프 발사 속도
    [SerializeField] private float _jumpAngle = 45.0f;     // 점프 발사 각도
    [SerializeField] private float _backJumpDistance = 5.0f; // 최대 백점프 수평 거리

    // 내부 변수들
    private Rigidbody2D _rb;
    private Vector2 _startPos;
    private Vector2 _jumpVelocity;

    protected override Status OnStart()
    {
        // Rigidbody2D 컴포넌트 얻기
        _rb = Self.Value.GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            return Status.Failure;
        }
        
        // 시작 위치 기록
        _startPos = Self.Value.transform.position;
        
        // 후방 방향 계산
        Vector2 backwardDir = new Vector2(-CurrentDirection.Value, 0).normalized;
        
        // 발사 각도 라디안으로 변환
        float radAngle = _jumpAngle * Mathf.Deg2Rad;
        
        // 초기 점프 속도(벡터) 계산  
        float vx = _jumpSpeed * Mathf.Cos(radAngle) * backwardDir.x;
        float vy = _jumpSpeed * Mathf.Sin(radAngle);
        _jumpVelocity = new Vector2(vx*2, vy/2);
        
        _rb.linearVelocity = _jumpVelocity;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 점프 진행 상황 판단
        float yDiff = Self.Value.transform.position.y- _startPos.y;
        if (_rb.linearVelocityY < 0)
        {
            _rb.gravityScale = 4f;
        }
        if (yDiff < 0.1f && Mathf.Abs(_rb.linearVelocity.y) < 0.1f)
        {
            // 착지 완료, 점프 종료
            return Status.Success;
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
        // 점프 종료 시 속도를 0으로 리셋
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.gravityScale = 1f;
        }
    }
}

