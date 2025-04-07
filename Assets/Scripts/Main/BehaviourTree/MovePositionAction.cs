using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MovePosition", story: "[Self] [BossSwordForcePos] [CurrentDirection]", category: "Action", id: "73ac63cad77644a89023da63e6221b24")]
public partial class MovePositionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<Vector2>> BossSwordForcePos;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;
    protected override Status OnStart()
    {
        Self.Value.transform.position = BossSwordForcePos.Value[1];
        if (CurrentDirection.Value > 0)
            Self.Value.transform.localScale = new Vector3(-1.3f, 1.3f, 1f);
        else
            Self.Value.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
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

