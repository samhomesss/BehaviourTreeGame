using Unity.Behavior;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    private BehaviorGraphAgent _behaviorGraphAgent;
    private Animator _animator;

    private void Start()
    {
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        _animator = GetComponent<Animator>();
        BossHpManager.BossHpDamageManager.OnEnemyDamagedEvent += Die;
    }

    private void Die(int bossHp)
    {
        if(bossHp <= 0)
        {
            _behaviorGraphAgent.enabled = false;
            _animator.Play("DEATH");
        }
    }
    
}
