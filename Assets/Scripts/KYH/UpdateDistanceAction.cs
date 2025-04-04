using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateDistance", story: "Update [Self] and [Target] [CurrentDistance]", category: "Action", id: "df19cc69ed9c0a7c580de1aaf9671ad6")]
public partial class UpdateDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        CurrentDistance.Value = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

