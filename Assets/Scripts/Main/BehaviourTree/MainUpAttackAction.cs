using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainUpAttack", story: "[Self] is [CurrentState] with Dir : [CurrentDirection] , Distance : [CurrentDistance]", category: "Action", id: "5520bf1b00118597594351c34c0099bc")]
public partial class MainUpAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;
    private Animator _animator;
    private int _animationHash;
    private Rigidbody2D _rb;
    private Vector2 dir;
    private BossAnimationEventController _controller;

    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(CurrentState.Value.ToString());
        _rb = Self.Value.GetComponent<Rigidbody2D>();
        _controller = Self.Value.GetComponent<BossAnimationEventController>();

        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("IsAttacking", true);
        if (CurrentDirection.Value > 0)
        {
            dir = new Vector2(1f, 11f);
        }
        else
        {
            dir = new Vector2(-1f, 11f);
        }

        _rb.AddForce(dir, ForceMode2D.Impulse);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(CurrentDistance.Value > 10f)
        {
            _controller.GetAttackSpecialEnd();
            // 아래대쉬베기 패턴으로 연결
            return Status.Failure;
        }

        if(_rb.linearVelocityY < 0)
        {
            _rb.gravityScale = 4f;
        }

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
        _rb.gravityScale = 1f;
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("IsAttacking", false);
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("CurrentState", MainBossState.IDLE);
    }
}

