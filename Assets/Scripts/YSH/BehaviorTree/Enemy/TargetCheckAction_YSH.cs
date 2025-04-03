using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TargetCheck", story: "Target Update", category: "Action", id: "b0710533ad789589d0b510e76e5ad6a8")]
public partial class TargetCheckAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    Player_YSH _player;

    protected override Status OnStart()
    {
        if ( _player == null)
            return Status.Success;
        _player = GameObject.FindAnyObjectByType<Player_YSH>();
        Target.Value = _player.gameObject;
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

