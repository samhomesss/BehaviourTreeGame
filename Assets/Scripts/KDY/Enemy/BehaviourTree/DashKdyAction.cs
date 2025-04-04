using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Dash_KDY", story: "[Self] Dash to [Target]", category: "Action", id: "5b9f8e69ac21c1f062513ed865ff1c28")]
public partial class DashKdyAction : Action
{
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    // 내부 상태
    private float elapsedTime; // 대시 경과 시간
    private Vector2 startPosition; // 시작 위치
    private Vector2 dashTargetPosition; // 대시 목표 위치
    protected override Status OnStart()
    {
        // 시작 위치, 타이머 초기화
        elapsedTime = 0f;
        startPosition = Self.Value.transform.position;
        
        // 대상의 앞쪽으로 대시
        Vector2 targetPos = Target.Value.transform.position;
        dashTargetPosition = new Vector2(targetPos.x, startPosition.y); // 수평
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        elapsedTime += Time.deltaTime;
        // 0 ~ 1 사이의 비율로 진행 상황 계산
        float t = Mathf.Clamp01(elapsedTime / dashDuration);
        
        // 선형 보간(Lerp)을 이용하여 위치 업데이트
        Vector2 newPosition = Vector2.Lerp(startPosition, dashTargetPosition, t);
        // z 좌표는 그대로 유지
        Self.Value.transform.position = new Vector3(newPosition.x, newPosition.y, Self.Value.transform.position.z);
        
        // N초 안에 도착하면 성공 반환
        if (t >= 1f)
        {
            return Status.Success;
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

