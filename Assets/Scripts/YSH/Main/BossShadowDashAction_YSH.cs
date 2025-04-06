using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
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

    Vector2 _dir;
    Transform _shadowTargetParent;
    Transform[] _shadowTarget;

    int _bossPos; // �ش� ������ BossShadowPos���� �� ��ġ�� ����  
    int _instnaceCount = 0;
    float _timer = 0;
    float _bossStrickTimer = 0;
    const float INSTANCE_TIMER = 4f;
    const float ATTACK_SPEED = 10f;


    // �ش� ������ �ൿ�� ���� �س����� �� ��?

    // coroutine���� �ϳ��� �� �ϸ� �� �� ?
    // �׸��� �ش� ������ �ൿ�� ������ �߰� �ϰ� 
    // �ش� �������� �ϳ� ��Ƽ� �ű⼭ �������

    protected override Status OnStart()
    {
        _bossShadow = Self.Value.GetComponent<BossInstantiateShadow>();
        _player = GameObject.FindAnyObjectByType<PlayerStateManager>();
        _bossStrickTimer = 0;
        _bossPos = UnityEngine.Random.Range(0, 4);
        Self.Value.transform.position = BossShadowPos.Value[_bossPos] + Vector2.down * 1.5f;
        _dir = (_player.transform.position - Self.Value.transform.position).normalized;

        // ī�޶� Ÿ�� ����
        _shadowTargetParent = GameObject.FindAnyObjectByType<ShadowPatternTarget>().transform;
        _shadowTarget = new Transform[_shadowTargetParent.childCount];
        for (int i = 0; i < _shadowTargetParent.childCount; i++)
        {
            _shadowTarget[i] = _shadowTargetParent.GetChild(i);
        }
        Managers.CameraTargetManager.RemoveTarget(Self.Value.transform);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_bossStrickTimer > 10f)
        {
            _bossStrickTimer = 0;
        }
        // ��ü For������ �����ϰ� Status.Success�� ��� ��Ű�� �ɵ�?
        _timer += Time.fixedDeltaTime;
        _bossStrickTimer += Time.fixedDeltaTime;

        if (_bossStrickTimer > 6f)
            Self.Value.transform.Translate(_dir * ATTACK_SPEED * Time.fixedDeltaTime);



        if (_timer > INSTANCE_TIMER)
        {
            if (_bossPos != _instnaceCount)
            {
                _bossShadow.InstantiateShadow(BossShadowPos.Value[_instnaceCount]);
                // �׸��ڵ� target �߰�
                for (int i = 0; i < _shadowTargetParent.childCount; i++)
                {
                    Managers.CameraTargetManager.AddTarget(_shadowTarget[i], 0.5f, 1f);
                }
            }

            if (_instnaceCount == 3) // �׸��� ���⿡ �ش� ������ ������ �� �������� �־���� �� ��?
            {
                _instnaceCount = 0;

                for (int i = 0; i < _shadowTargetParent.childCount; i++)
                {
                    Managers.CameraTargetManager.RemoveTarget(_shadowTarget[i]);
                }

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
}

