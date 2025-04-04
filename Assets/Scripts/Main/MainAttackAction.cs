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
        // 애니메이션 명칭이 문자열로 그대로 들어가기 때문에
        // 휴먼에러 조심해야합니다.
        _animationHash = Animator.StringToHash("Attack");
        //////////////////  Warning  /////////////////

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 애니메이션 길이가 Success의 트리거가 됩니다.
        // 따라서 Attack 애니메이션은 Exit로 연결해주어야 합니다.
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

