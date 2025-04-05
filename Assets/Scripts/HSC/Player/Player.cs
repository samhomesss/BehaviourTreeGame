using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _life = 3; // Health of the boss
    [SerializeField] private int _maxLife = 3; // Maximum health of the boss

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // im si ro man den layer name
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossAttack"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _life--;
        if (_life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ///////////////////
        // Todo : 입력정지 코드
        ////////////////

        _animator.Play("DEATH");
    }
}
