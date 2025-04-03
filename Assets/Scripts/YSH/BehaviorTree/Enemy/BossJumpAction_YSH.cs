using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossJump", story: "[Self] is Jump", category: "Action", id: "c8df11db5b3e4de269b0552c8fa866b9")]
public partial class BossJumpAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    Rigidbody2D _rigid;
    const float JumpForce = 20f;
    protected override Status OnStart()
    {
        _rigid = Self.Value.GetComponent<Rigidbody2D>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _rigid.AddForce(Vector2.up * JumpForce , ForceMode2D.Impulse);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

