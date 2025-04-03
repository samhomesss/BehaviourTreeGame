using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossIsAttack_YSH", story: "[Self] is Attack", category: "Action", id: "dca7f77f5faa479600ca5ddb6c683d41")]
public partial class BossIsAttackAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Debug.Log("공격 상태 입니다.");
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

