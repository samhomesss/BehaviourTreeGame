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
    protected override Status OnStart()
    {
        _rigid = GameObject.GetComponent<Rigidbody2D>();
        Debug.Log($"Target : {Target.Value}");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Todo: µøøµµøøµ¿Ã∞° ø©±‚ø° ππ «ÿ¡‡æﬂ µ  
        Vector2 dist = (Vector2)(Target.Value.transform.position - Self.Value.transform.position).normalized;
        _rigid.AddForce(dist * 2 , ForceMode2D.Impulse);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

