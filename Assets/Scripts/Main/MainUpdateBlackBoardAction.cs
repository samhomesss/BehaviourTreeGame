using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainUpdateBlackBoard", story: "[Self] [Target] [CurrentDirection] isAttack : [isAttacking]", category: "Action", id: "b6f8ce9906bd6005dbedca923e31e5be")]
public partial class MainUpdateBlackBoardAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;
    [SerializeReference] public BlackboardVariable<bool> IsAttacking;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Filp
        CurrentDirection.Value = Target.Value.transform.position.x - Self.Value.transform.position.x;
        if(!IsAttacking.Value)
        {
            if (CurrentDirection.Value > 0)
                Self.Value.GetComponent<SpriteRenderer>().flipX = false;
            else
                Self.Value.GetComponent<SpriteRenderer>().flipX = true;
        }

        return Status.Success;
    }
}

