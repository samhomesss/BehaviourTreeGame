using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateCurrentDis", story: "Update [CurrentDis] by [Self] and [Target]", category: "Action", id: "439d494eb493c6d68fcd021c31acd18a")]
public partial class UpdateCurrentDisAction : Action
{
    [SerializeReference] public BlackboardVariable<float> CurrentDis;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        CurrentDis.Value = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

