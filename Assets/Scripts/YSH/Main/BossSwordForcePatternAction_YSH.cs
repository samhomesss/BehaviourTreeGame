using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossSwordForcePattern", story: "[Self] is [CurrentState] and go [BossSwordForcePos]", category: "Action", id: "9b7be3914f52f8f132b567fa097e5614")]
public partial class BossSwordForcePatternAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    [SerializeReference] public BlackboardVariable<List<Vector2>> BossSwordForcePos;
    //Todo: 검기패턴 백스텝 뒤로 할거면 위치만 빼면 됨 -> 추후 작업 보고 결정 
    BossInstanctiateSwordForce _instanceSword;

    float _timer = 0;
    float _swordForceInstanceTime = 0;

    protected override Status OnStart()
    {
        int random = UnityEngine.Random.Range(0 , 2);
        // Todo : 거리 계산해서 두 값 계산해서 더 먼 쪽으로 이동시키기
        // OR : 포지션 이동 없이 백스텝 이후 패턴으로 넣거나.
        //Self.Value.transform.position = BossSwordForcePos.Value[random];
        _instanceSword = Self.Value.GetComponent<BossInstanctiateSwordForce>();

        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        _timer += Time.fixedDeltaTime;
        _swordForceInstanceTime += Time.fixedDeltaTime;

        if (_swordForceInstanceTime > 3f)
        {
            // Todo : 검기 소환 해야됨 
            _instanceSword.InstantiateSword();
            _swordForceInstanceTime = 0;
        }

        if (_timer > 9f)
        {
            _timer = 0;
            _swordForceInstanceTime = 0;
            return Status.Success;
        }

        return Status.Running;

    }

    protected override void OnEnd()
    {
    }
}

