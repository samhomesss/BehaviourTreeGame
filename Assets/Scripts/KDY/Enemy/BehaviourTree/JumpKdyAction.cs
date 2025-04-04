using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Jump_KDY", story: "[Self] Jump to [Target]", category: "Action", id: "3fe71dae29113dde796d5a6134ffa582")]
public partial class JumpKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeField] private float _flightTime = 1.5f; // 점프 시간
    [SerializeField] private float _gravity = 9.81f;
    
    // 내부 변수들
    private float _elapsedTime;
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _v_x;
    private float _v_y;

    protected override Status OnStart()
    {
        // 초기화: 시작 위치와 목표 위치, 그리고 타이머 초기화
        _elapsedTime = 0f;
        _startPos = Self.Value.transform.position;
        _targetPos = Target.Value.transform.position;
        
        // 수평, 수직 초기 속도 계산 (비행 시간 T를 기준으로)
        _v_x = (_targetPos.x - _startPos.x) / _flightTime;
        _v_y = (_targetPos.y - _startPos.y + 0.5f * _gravity * _flightTime * _flightTime) / _flightTime;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 매 프레임마다 경과 시간 업데이트
        _elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp(_elapsedTime, 0f, _flightTime);
        
        // 포물선 운동 공식을 이용한 위치 계산
        float newX = _startPos.x + _v_x * t;
        float newY = _startPos.y + _v_y * t - 0.5f * _gravity * t * t;
        
        // 보스의 위치 업데이트
        Self.Value.transform.position = new Vector3(newX, newY, Self.Value.transform.position.z);

        // 점프가 끝났으면 Success 반환
        if (_elapsedTime >= _flightTime)
        {
            return Status.Success;
        }
        
        return Status.Running;
    }
}

