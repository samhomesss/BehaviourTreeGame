using Unity.Behavior;
using UnityEngine;

/// <summary>
/// ���� ������Ʈ�� ������Ʈ�� �߰����ֽø� �˴ϴ�.
/// </summary>
public class BossController_HSC : MonoBehaviour
{
    private BehaviorGraphAgent _behaviorGraphAgent;
    private Animator _animator;
    private BossState _currentState; // ���� ���¸� ������ ����
    public BossState CurrentState
    {
        get => _currentState;
        set
        {
            if (value != _currentState)
            {
                _currentState = value;
                
                // �ִϸ��̼� �̸��� ���� �̸��� ���ٰ� ����
                // �Ʒ� �ּ� ó���� �κп� �ִϸ��̼� Ʈ���Ÿ� �������ֽø� �˴ϴ�.
                // ���� �̸��� �ִϸ��̼� �̸��� �ٸ���� �����߻��ϹǷ� �ϴ� �ּ�ó���صξ����ϴ�.
                _animator.Play(value.ToString());
            }
        }
    }

    private void Start()
    {
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �������� ������ġ��
        _behaviorGraphAgent.GetVariable("BossState", out BlackboardVariable<BossState> _currentState);
        CurrentState = _currentState.Value; 
    }
}
