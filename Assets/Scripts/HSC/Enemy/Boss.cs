using Unity.Behavior;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float _health = 100f; // Health of the boss
    [SerializeField] private float _maxHealth = 100f; // Maximum health of the boss

    // player damage is always same. he has only one attack way
    [SerializeField] private float _damage = 10f;

    private BehaviorGraphAgent _behaviorGraphAgent;
    private Animator _animator;

    private void Start()
    {
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        _animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // im si ro man den layer name
        if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health -= _damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _behaviorGraphAgent.enabled = false;
        _animator.Play("DEATH");
    }
}
