using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.AppUI.Core;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossShadowDashAction", story: "[Self] is [CurrentState] and go [BossShadowPos]", category: "Action", id: "3969cd584d42bcf82b6661fd5bd3dacf")]
public partial class BossShadowDashAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;
    [SerializeReference] public BlackboardVariable<List<Vector2>> BossShadowPos;

    PlayerStateManager _player;
    BossInstantiateShadow _bossShadow;

    Vector2 _bossDownStrickPos = new Vector2 (0f , 4f); // 보스가 떨어질 포지션 
    Vector2 _bossDownStrickTarget = new Vector2(0f, -3.4f);
    Vector2 dir;
    Vector2 _attackDir;
    int _bossPos; // 해당 보스가 BossShadowPos에서 갈 위치를 정함  
    int _instnaceCount = 0;
    float _timer = 0;
    float _bossStrickTimer = 0;
    const float DOWN_STRICK_SPEED = 0.05f; 
    const float INSTANCE_TIMER = 4f;
    const float ATTACK_SPEED = 10f;
    // 해당 보스의 행동을 정의 해놓으면 될 듯?

    // Corutain으로 하나씩 더 하면 될 듯 ?
    // 그리고 해당 보스의 행동에 돌진을 추가 하고 
    // 해당 포지션을 하나 잡아서 거기서 내려찍기

    protected override Status OnStart()
    {
        _bossShadow = Self.Value.GetComponent<BossInstantiateShadow>();
        _player = GameObject.FindAnyObjectByType<PlayerStateManager>();
        _bossStrickTimer = 0;
        _bossPos = UnityEngine.Random.Range(0, 4);
        Self.Value.transform.position = BossShadowPos.Value[_bossPos] + Vector2.down * 1.5f;
        dir = (_player.transform.position - Self.Value.transform.position).normalized;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_bossStrickTimer > 10f)
        {
            _bossStrickTimer = 0;
        }
        // 전체 For문으로 생각하고 Status.Success를 통과 시키면 될듯?
        _timer += Time.fixedDeltaTime;
        _bossStrickTimer += Time.fixedDeltaTime;

        if (_bossStrickTimer > 6f)
        {
            Self.Value.transform.Translate(dir * ATTACK_SPEED * Time.fixedDeltaTime);
            
        }

        if (_timer > INSTANCE_TIMER)
        {
            if (_bossPos == _instnaceCount) { }
            else
                _bossShadow.InstantiateShadow(BossShadowPos.Value[_instnaceCount]);

            if (_instnaceCount == 3) // 그리고 여기에 해당 보스의 패턴이 다 끝났으면 넣어줘야 할 듯?
            {
                _instnaceCount = 0;
                return Status.Success;
            }

            _instnaceCount++;
            _timer = 0;
        }

        

        return Status.Running;
    }

    protected override void OnEnd()
    {

    }

    IEnumerator BossDashtoPlayer()
    {
        dir = (_player.transform.position + Vector3.up * 1.5f - Self.Value.transform.position).normalized;
        Self.Value.transform.Translate( dir * ATTACK_SPEED * Time.deltaTime);
        yield return new WaitForSeconds(3f);
        Self.Value.transform.position = _bossDownStrickPos;
        yield return new WaitForSeconds(2f);
        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, _bossDownStrickTarget, DOWN_STRICK_SPEED);
        yield return new WaitForSeconds(1f);
    }

    void BossDropPattern()
    {
        
        Self.Value.transform.position = _bossDownStrickPos;
        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, _bossDownStrickTarget, DOWN_STRICK_SPEED);
    }
}

