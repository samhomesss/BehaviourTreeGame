using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayAnimation_KDY", story: "[Self] Play [State] Animator", category: "Action", id: "81560f884849a61f695c83d0d037d079")]
public partial class PlayAnimationKdyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<currentAnimationState> State;
    
    private Animator _animator;
    private int _animationHash;

    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(State.Value.ToString());
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(_animationHash == _animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }

    protected override void OnEnd()
    {
    }
}

