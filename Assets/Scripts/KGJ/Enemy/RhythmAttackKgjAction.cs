using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RhythmAttack_KGJ", story: "[Self] is [CurrentState]", category: "Action", id: "8e4b26cd6da29d030551f6d689963a0d")]
public partial class RhythmAttackKgjAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    Animator _animator;
    int _animationHash;

    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(CurrentState.Value.ToString());
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("IsAttacking", true);
        return Status.Running;
    }

    // ��� �ִϸ��̼� ���۰� ������ �̺�Ʈ�� �޾Ƽ� �������� �ݴϴ�.
    // �ش� �ڵ�� BossAnimationEventController�� �ֽ��ϴ�.
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
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("CurrentState", MainBossState.IDLE);
    }
}

