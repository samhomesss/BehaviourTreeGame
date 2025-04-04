using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Jump", story: "[Self] Jumping with [Direction] [JumpForce] [Gravity]", category: "Action", id: "fd6a0ac8fe6853687f513557b6c85d2a")]
public partial class JumpAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector2> Direction;
    [SerializeReference] public BlackboardVariable<float> JumpForce;
    [SerializeReference] public BlackboardVariable<float> Gravity;

    private Rigidbody2D _rb;
    private Vector2 dir;

    protected override Status OnStart()
    {
        _rb = Self.Value.GetComponent<Rigidbody2D>();
        dir = new Vector2(Direction.Value.x * 0.5f, 1);
        if (Direction.Value.x > 0)
            Self.Value.transform.localScale = new Vector3(1, 1, 1);
        else
            Self.Value.transform.localScale = new Vector3(-1, 1, 1);

        _rb.AddForce(dir * JumpForce.Value, ForceMode2D.Impulse);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

