using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossJumpDash", story: "[Self] is Dash to [Target]", category: "Action", id: "9b6752624f28305d27b8efa00866d428")]
public partial class BossJumpDashAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> MaxHeight;

    Rigidbody2D _rigid;
    Vector2 _targetPos;
    const float Power = 150f;
    const float LerpTime = 1f;

    protected override Status OnStart()
    {
        _rigid = Self.Value.GetComponent<Rigidbody2D>();
        Target.Value = GameObject.FindAnyObjectByType<Player_YSH>().gameObject;
        _targetPos = Target.Value.transform.position;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        Vector2 dir = (Vector2)(Target.Value.transform.position - Self.Value.transform.position).normalized;
        float dist = (Target.Value.transform.position - Self.Value.transform.position).magnitude;
        //_rigid.AddForce(dir * Power, ForceMode2D.Impulse);
        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position , _targetPos , LerpTime);

        if (dist < 1.5f)
        {
            _rigid.angularVelocity = 0;
            return Status.Success;
        }


        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

