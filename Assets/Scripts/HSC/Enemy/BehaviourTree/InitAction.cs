using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Init", story: "[Self] [JumpHeight] [JumpTimeToApex]", category: "Action", id: "8e8a72a91e1e56f70b3a99c88454c8d7")]
public partial class InitAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> JumpHeight;
    [SerializeReference] public BlackboardVariable<float> JumpTimeToApex;

    private float _gravityStrength;

    protected override Status OnStart()
    {
        _gravityStrength = -(2 * JumpHeight.Value) / Mathf.Pow(JumpTimeToApex.Value, 2);
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("Gravity", _gravityStrength/ Physics2D.gravity.y);
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("JumpForce", Mathf.Abs(_gravityStrength) * JumpTimeToApex.Value);

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

