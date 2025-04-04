using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move_KDY", story: "[Self] Move to [Target]", category: "Action", id: "13fd447682b7e0f863796b531c4909c0")]
public partial class MoveKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    private float _speed = 1.0f;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // x좌표만 대상에게 이동
        
        Vector2 targetPosition = Target.Value.transform.position;
        Vector2 selfPosition = Self.Value.transform.position;
        Vector2 direction = (targetPosition - selfPosition).normalized;
        Vector2 newPosition = selfPosition + direction * _speed * Time.deltaTime;
        newPosition.y = selfPosition.y; // y좌표는 고정
        Self.Value.transform.position = newPosition;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

