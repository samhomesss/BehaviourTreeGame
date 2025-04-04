using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "New Action", story: "Play Attack", category: "Action/Animation", id: "2670b4f6a9d091540e08834aed2a7d42")]
public partial class NewAction : Action
{

    protected override Status OnStart()
    {
        
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

