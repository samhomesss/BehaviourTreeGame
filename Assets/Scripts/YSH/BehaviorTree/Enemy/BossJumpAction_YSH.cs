using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossJump", story: "[Self] is Jump", category: "Action", id: "c8df11db5b3e4de269b0552c8fa866b9")]
public partial class BossJumpAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> MaxHeight;
   
    Rigidbody2D _rigid;
    const float JumpForce = 20f;
    float _startYPos;
    const float FallDownGravity = 10f;

    protected override Status OnStart()
    {
        _rigid = Self.Value.GetComponent<Rigidbody2D>();
        _startYPos = Self.Value.gameObject.transform.position.y;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _rigid.AddForce(Vector2.up * JumpForce , ForceMode2D.Impulse);

        float nowHeight = Self.Value.transform.position.y;
        if ((_startYPos - nowHeight) > MaxHeight)
        {
            _rigid.AddForce(Vector2.down * FallDownGravity, ForceMode2D.Impulse);
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {

    }
}

