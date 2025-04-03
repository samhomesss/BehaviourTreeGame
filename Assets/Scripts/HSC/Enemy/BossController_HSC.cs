using Unity.Behavior;
using UnityEngine;

public class BossController_HSC : MonoBehaviour
{
    private BehaviorGraphAgent _behaviorGraphAgent;
    private Animator _animator;
    private BossState _currentState; // 현재 상태를 저장할 변수
    public BossState CurrentState
    {
        get => _currentState;
        set
        {
            if (value != _currentState)
            {
                _currentState = value;
                
                // 애니메이션 이름과 상태 이름이 같다고 가정
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
        // 매프레임 제물바치기
        _behaviorGraphAgent.GetVariable("BossState", out BlackboardVariable<BossState> _currentState);
        CurrentState = _currentState.Value; 
    }
}
