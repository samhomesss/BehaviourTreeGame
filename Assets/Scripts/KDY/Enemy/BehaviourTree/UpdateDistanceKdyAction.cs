using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateDistance_KDY", story: "Update [Self] and [Target] [CurrentDistance]", category: "Action", id: "f39bcb0dd95ea46f0d7d799a499b88e6")]
public partial class UpdateDistanceKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;

    protected override Status OnUpdate()
    {
        CurrentDistance.Value = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);
        return Status.Success;
    }
}

