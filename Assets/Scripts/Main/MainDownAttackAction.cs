using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainDownAttack", story: "[Self] is [CurrentState] with Dir : [CurrentDirection]", category: "Action", id: "708924b1c9fe533e53a256b51a33ad27")]
public partial class MainDownAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;
    private Animator _animator;
    private int _animationHash;
    private Rigidbody2D _rb;
    private Vector2 dir;

    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(CurrentState.Value.ToString());
        _rb = Self.Value.GetComponent<Rigidbody2D>();

        if (CurrentDirection.Value > 0)
        {
            dir = new Vector2(30f, -25f);
        }
        else
        {
            dir = new Vector2(-30f, -25f);
        }

        _rb.AddForce(dir, ForceMode2D.Impulse);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_animationHash == _animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
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
        _rb.linearVelocity = Vector2.zero;
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("CurrentState", MainBossState.IDLE);
    }
}

