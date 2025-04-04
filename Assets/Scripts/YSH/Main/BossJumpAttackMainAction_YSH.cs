using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossJumpAttack_Main", story: "[Self] is [CurrentState]", category: "Action", id: "095dacc6380fb8ec30a388ebb21c3658")]
public partial class BossJumpAttackMainAction_YSH : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<MainBossState> CurrentState;

    const float JumpHeight = 1.5f; // ���� ���� 
    const float JumpSpeed = 0.05f; // �ö󰡴� �ӵ�
    const float FallSpeed = 0.07f;  // �������� �ӵ� 
    const float JumpDuration = 5f; // Duration
    const float FallDuration = 3f; // Duration

    Vector2 _jumpStartTargetPos; // ���� �����Ҷ� �÷��̾� �Ӹ� ���� �̵��� ���� 
    Vector2 _jumpAttackTarget; // �������� ���� 
    float _timer; // �ð� ���� �������°� ���
    float _bossOriginalPosY = -3.5f; // ������ ������� ������ ����
    bool _isFalling; // �������� ��
    PlayerStateManager player; // �÷��̾� ���� 


    protected override Status OnStart()
    {
        player = GameObject.FindAnyObjectByType<PlayerStateManager>();
        if (player == null) return Status.Failure; // �÷��̾� ������ �ٷ� ���� ��ȯ 

        _jumpStartTargetPos = new Vector2(player.transform.position.x, JumpHeight); // �÷��̾� ���� ���� ���� 
        _timer = 0f; // �ð� �ʱ�ȭ 
        _isFalling = false; // ���� �ȶ����� 

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _jumpStartTargetPos = new Vector2(player.transform.position.x, JumpHeight); // �������� ��� ���� �ϱ� ����

        if (!_isFalling) // �ö󰡴� ����
        {
            UpdateJumpPhase();
        }
        else // �������� ���� 
        {
            return UpdateFallPhase();
        }

        return Status.Running;
    }

    // �ö� �� 
    private void UpdateJumpPhase()
    {
        _timer += Time.fixedDeltaTime; // �ð� �÷��ְ� 

        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, _jumpStartTargetPos, JumpSpeed); // �ö󰡴� �ð� ���� Lerp

        if (_timer >= JumpDuration) // �������� �Ǿ��� ������
        {
            _isFalling = true; // �������°� ����� �ְ� 
            _timer = 0f; // Timer = 0 ���� ������ְ� 

            _jumpAttackTarget = new Vector2(_jumpStartTargetPos.x, _bossOriginalPosY); //�÷��̾� ��ġ �� ������ ���� Y ������ Attack Target ����
        }
    }
    // ������ �� 
    private Status UpdateFallPhase()
    {

        _timer += Time.fixedDeltaTime; // �ð� �ø��� 

        Vector2 fallTarget = new Vector2(_jumpAttackTarget.x, _bossOriginalPosY); // ���� ��ġ�� �޾Ƽ� �������� Ÿ������ ��� 
        //Vector2 fallTarget = new Vector2(_jumpStartTargetPos.x, _bossOriginalPosY); // ���� ��ġ�� �޾Ƽ� �������� Ÿ������ ��� 
        float fallProgress = Mathf.Clamp01(_timer / FallDuration); 

        Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, fallTarget, FallSpeed);

        if (fallProgress >= 1f) // �������� �ð� �����߸� 
        {
            Self.Value.transform.position = fallTarget; // ��Ȯ�� ��ǥ ��ġ�� ����
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        _timer = 0f;
        _isFalling = false;
    }
}


