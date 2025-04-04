using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MainAttack", story: "[Self] is [CurrentState]", category: "Action", id: "9521040d0580efd64cfdf93f7560b090")]
public partial class MainAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    private Animator _animator;
    private int _animationHash;

    protected override Status OnStart()
    {
        _animator = Self.Value.GetComponent<Animator>();

        //////////////////  Warning  /////////////////
        // �ִϸ��̼� ��Ī�� ���ڿ��� �״�� ���� ������
        // �޸տ��� �����ؾ��մϴ�.
        Debug.Log(CurrentState.Value.ToString());
        _animationHash = Animator.StringToHash(CurrentState.Value.ToString());
        //////////////////  Warning  /////////////////

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // �ִϸ��̼� ���̰� Success�� Ʈ���Ű� �˴ϴ�.
        // ���� Attack �ִϸ��̼��� Exit�� �������־�� �մϴ�.
        if (_animationHash == _animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }
}

