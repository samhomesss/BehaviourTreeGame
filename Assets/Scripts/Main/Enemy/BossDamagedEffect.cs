using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class BossDamagedEffect : MonoBehaviour
{
    private BehaviorGraphAgent _behaviorGraphAgent;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Color damagedColor = new Color(1, 0.6f, 0.6f, 1);

    private void Start()
    {
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        BossHpManager.BossHpDamageManager.OnEnemyDamagedEvent += SetBlackBoardBossHp;
        BossHpManager.BossHpDamageManager.OnEnemyDamagedEvent += Die;
        BossHpManager.BossHpDamageManager.OnEnemyDamagedEvent += _ => DamagedEffect();
    }

    private void SetBlackBoardBossHp(int bossHp)
    {
        _behaviorGraphAgent.SetVariableValue("BossHp", bossHp);
    }

    private void Die(int bossHp)
    {
        if(bossHp <= 0)
        {
            _behaviorGraphAgent.enabled = false;
            _animator.Play("DEATH");
        }
    }

    private void DamagedEffect()
    {
        StartCoroutine(DamagedEffectCoroutine());
    }

    IEnumerator DamagedEffectCoroutine()
    {
        _spriteRenderer.color = damagedColor;
        float time = 0f;
        while (time < 0.3f)
        {
            float t = time / 0.3f;
            float gb = Mathf.Lerp(150f / 255f, 1f, t);
            _spriteRenderer.color = new Color(1,gb,gb,1);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
