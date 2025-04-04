using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateIsFaceLeft", story: "Update [IsFaceLeft] with [Self] and [Target]", category: "Action", id: "bd0bce8475c0993034930f3e0e8d069e")]
public partial class UpdateIsFaceLeftAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsFaceLeft;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        IsFaceLeft.Value = Self.Value.transform.position.x <= Target.Value.transform.position.x;
        return Status.Success;
    }
}

