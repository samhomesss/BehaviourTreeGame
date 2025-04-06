using Unity.Behavior;
using UnityEngine;

/// <summary>
/// 보스 오브젝트에 컴포넌트로 추가해주시면 됩니다.
/// </summary>
public class BossController_HSC : MonoBehaviour
{
    private BehaviorGraphAgent _behaviorGraphAgent;
    private Animator _animator;
    private MainBossState _currentState; // 현재 상태를 저장할 변수
    public MainBossState CurrentState
    {
        get => _currentState;
        set
        {
            if (value != _currentState)
            {
                _currentState = value;
                Debug.Log("실행 "+value.ToString());
                // 애니메이션 이름과 상태 이름이 같다고 가정
                // 아래 주석 처리한 부분에 애니메이션 트리거를 설정해주시면 됩니다.
                // 상태 이름과 애니메이션 이름이 다를경우 에러발생하므로 일단 주석처리해두었습니다.
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
        _behaviorGraphAgent.GetVariable("CurrentState", out BlackboardVariable<MainBossState> _currentState);
        CurrentState = _currentState.Value; 
    }
}
