using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "BossIsGrounded", story: "[Self] is [_isGround]", category: "Conditions", id: "dc24ec4580dfb9e2057679ce2859d8d5")]
public partial class BossIsGroundedCondition_YSH : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> IsGround;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
