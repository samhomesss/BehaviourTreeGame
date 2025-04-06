using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainMove", story: "[Self] moves [MoveSpeed] [CurrentDistance] in Range : [range] to [CurrentDirection]", category: "Action", id: "2faa82b59861249f8dadea1bad19387b")]
public partial class MainMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;
    Rigidbody2D _rigidbody;

    protected override Status OnStart()
    {
        _rigidbody = Self.Value.GetComponent<Rigidbody2D>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CurrentDistance.Value < Range)
        {
            return Status.Failure;
        }
        
        if (CurrentDirection.Value > 0)
        {
            _rigidbody.linearVelocity = new Vector2(1, 0) * MoveSpeed;
        }
        else if (CurrentDirection.Value < 0)
        {
            _rigidbody.linearVelocity = new Vector2(-1, 0) * MoveSpeed;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }
}

