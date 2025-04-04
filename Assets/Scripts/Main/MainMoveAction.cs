using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainMove", story: "[Self] moves by [IsFaceLeft] and [MoveSpeed]", category: "Action", id: "2faa82b59861249f8dadea1bad19387b")]
public partial class MainMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    Rigidbody2D _rigidbody;

    protected override Status OnStart()
    {
        _rigidbody = Self.Value.GetComponent<Rigidbody2D>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_rigidbody.transform.localScale.x < 0)
        {
            _rigidbody.linearVelocity = new Vector2(1, 0) * MoveSpeed;
        }
        else if (_rigidbody.transform.localScale.x > 0)
        {
            _rigidbody.linearVelocity = new Vector2(-1, 0) * MoveSpeed;
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }
}

