using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AlwaysFail", story: "Always Fail", category: "Action", id: "9667ad5c3a180f3a16c398b07b687eff")]
public partial class MainAlwaysFailAction : Action
{

    protected override Status OnStart()
    {
        return Status.Failure;
    }
}

