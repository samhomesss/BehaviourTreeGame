using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Self] do Attack [Player] with [direction]", category: "Action", id: "3c56537b82c659fd5b6c971d14afd780")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<Vector2> Direction;
    private Animator _animator;
    private int _animationHash;

    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        _animationHash = Animator.StringToHash("Attack");

        // filp
        if (Direction.Value.x > 0)
            Self.Value.transform.localScale = new Vector3(1, 1, 1);
        else
           Self.Value.transform.localScale = new Vector3(-1, 1, 1);
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
}

