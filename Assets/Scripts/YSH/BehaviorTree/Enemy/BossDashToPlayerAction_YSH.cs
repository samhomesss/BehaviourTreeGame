using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossDashToPlayer", story: "[Self] is Dash to [Target]", category: "Action", id: "8722731494ca1dfa2b8f8b9efd8d3544")]
public partial class BossDashToPlayerAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    Rigidbody2D _rigid;
    float _maxValocity = 5f;
    Vector2 _moveStartDir;
    Vector2 dist;
    protected override Status OnStart()
    {
        _rigid = Self.Value.GetComponent<Rigidbody2D>();
        Debug.Log($"Target : {Target.Value}");
        dist = (Vector2)(Target.Value.transform.position - Self.Value.transform.position).normalized;
        _moveStartDir = dist;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        dist = (Vector2)(Target.Value.transform.position - Self.Value.transform.position).normalized;

        _rigid.AddForce(dist * 2 , ForceMode2D.Impulse);

        if (_rigid.angularVelocity > _maxValocity)
        {
            _rigid.angularVelocity = _maxValocity;
        }

        float dotProduct = Vector2.Dot(dist, _moveStartDir);

        if (dotProduct < 0f)
        {
            _rigid.linearVelocity = Vector2.zero;
            _rigid.angularVelocity = 0f;
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

