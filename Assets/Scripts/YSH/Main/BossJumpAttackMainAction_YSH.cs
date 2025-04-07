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

    const float JumpHeight = 2f; // ���� ���� 
    const float JumpSpeed = 0.05f; // �ö󰡴� �ӵ�
    const float JumpDuration = 8f; // Duration

    Vector2 _jumpStartTargetPos; // ���� �����Ҷ� �÷��̾� �Ӹ� ���� �̵��� ���� 
    Vector2 _jumpAttackTarget; // �������� ���� 
    float _timer; // �ð� ���� �������°� ���
    float _bossOriginalPosY = -3.75f; // ������ ������� ������ ����
    bool _isFalling; // �������� ��
    PlayerStateManager player; // �÷��̾� ���� 
    Rigidbody2D _rb;

    protected override Status OnStart()
    {
        player = GameObject.FindAnyObjectByType<PlayerStateManager>();
        if (player == null) return Status.Failure; // �÷��̾� ������ �ٷ� ���� ��ȯ 
        _rb = Self.Value.GetComponent<Rigidbody2D>();

        _jumpStartTargetPos = new Vector2(player.transform.position.x, JumpHeight); // �÷��̾� ���� ���� ���� 
        _timer = 0f; // �ð� �ʱ�ȭ 
        _isFalling = false; // ���� �ȶ����� 

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!_isFalling) // �ö󰡴� ����
        {
            _jumpStartTargetPos = new Vector2(player.transform.position.x, JumpHeight); // �������� ��� ���� �ϱ� ����
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
            Debug.Log("�������� ����");
            _isFalling = true; // �������°� ����� �ְ� 
            _timer = 0f; // Timer = 0 ���� ������ְ� 

            _jumpAttackTarget = new Vector2(_jumpStartTargetPos.x, _bossOriginalPosY); //�÷��̾� ��ġ �� ������ ���� Y ������ Attack Target ����
        }
    }
    // ������ �� 
    private Status UpdateFallPhase()
    {
        //_timer += Time.fixedDeltaTime; // �ð� �ø��� 

        //Self.Value.transform.position = Vector2.Lerp(Self.Value.transform.position, _jumpAttackTarget, _timer/FallDuration);

        //if (_timer >= FallDuration) // �������� �ð� �����߸� 
        //{
        //    //Self.Value.transform.position = fallTarget; // ��Ȯ�� ��ǥ ��ġ�� ����
        //    return Status.Success;
        //}
        _rb.gravityScale = 5f;

        if(Mathf.Abs(Self.Value.transform.position.y - _bossOriginalPosY) < 0.5f)
        {
            return Status.Success; // ���� ��ġ�� �����ϸ� ����
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
        _timer = 0f;
        _rb.gravityScale = 1f; // �߷� �ʱ�ȭ
        _isFalling = false;
        Self.Value.GetComponent<BehaviorGraphAgent>().SetVariableValue("CurrentState", MainBossState.IDLE); // ���� ���� ����
    }
}


