using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Idle", story: "[self] is [BossState]", category: "Action", id: "d741c18190c8527835cd75a5a0d6b326")]
public partial class IdleAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BossState> BossState;
    private Animator _animator;
    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        Debug.Log("IdleAction Start");
        //_animator.SetTrigger("Idle");
        return Status.Success;
    }
}

