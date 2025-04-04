using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainIdle", story: "[Self] is [CurrentState] [CurrentDistance] in Range : [range]", category: "Action", id: "eaa04511bbb79697efc3c1344f97b0a1")]
public partial class MainIdleAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;
    [SerializeReference] public BlackboardVariable<float> Range;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CurrentDistance.Value < Range)
        {
            return Status.Failure;
        }

        return Status.Running;
    }
}

