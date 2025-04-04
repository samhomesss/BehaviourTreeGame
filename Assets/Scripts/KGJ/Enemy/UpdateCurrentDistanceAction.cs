using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateCurrentDistance", story: "Update [CurrentDistance] with [Self] and [Target]", category: "Action", id: "579aa2a49640dbc22843ca19f8242059")]
public partial class UpdateCurrentDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        float distance = Vector3.Distance(Self.Value.transform.position, Target.Value.transform.position);
        CurrentDistance.Value = distance;
        return Status.Success;
    }
}

