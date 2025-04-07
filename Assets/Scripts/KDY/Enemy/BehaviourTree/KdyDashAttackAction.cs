using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "KDY_DashAttack", story: "[Self] is [CurrentState] with [CurrentDirection]", category: "Action", id: "a80ff7cc647129650017ae854f2075c9")]
public partial class KdyDashAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    [SerializeReference] public BlackboardVariable<float> CurrentDirection;
    private Animator _animator;
    private int _animationHash;
    private Rigidbody2D _rb;
    
    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(CurrentState.Value.ToString());
        _rb = Self.Value.GetComponent<Rigidbody2D>();
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("IsAttacking", true);
        
        // 플레이어 방향으로 대쉬 공격
        if (_rb != null)
        {
            Vector2 dashDirection = new Vector2(CurrentDirection.Value, 0).normalized;
            float dashSpeed = 20f; // 대쉬 속도
            _rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
        }
        else
        {
            return Status.Failure;
        }

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
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("IsAttacking", false);
        _rb.linearVelocity = Vector2.zero;
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("CurrentState", MainBossState.IDLE);
    }
}
