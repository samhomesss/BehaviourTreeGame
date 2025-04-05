using UnityEngine;

public class BossShadowLookPlayer : MonoBehaviour
{
    PlayerStateManager _player;
    SpriteRenderer _sprite;

    GameObject _bossShadowStartPrefab; 

    Vector2 dir;
    Vector2 _attackDir; // 공격 방향 설정 
    bool _isAttack = false;
    float _dirTimer = 0; // 플레이어 방향을 계산 하기 위함

    const float ATTACK_SPEED = 60f;
    void Start()
    {
        _player = FindAnyObjectByType<PlayerStateManager>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _bossShadowStartPrefab = Resources.Load<GameObject>("YSH/Effect/BossShadow_StartParticle");

        dir = (_player.transform.position + Vector3.up * 1.5f - transform.position).normalized;

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }

        GameObject go = Instantiate(_bossShadowStartPrefab, transform.position, Quaternion.identity);
        Destroy(go, 1f);
    }

    void Update()
    {
        if (_isAttack)
        {
            transform.Translate(Vector3.left * ATTACK_SPEED * Time.deltaTime);
            Destroy(gameObject, 3f);
        }

        if (!_isAttack)
        {
            _dirTimer += Time.deltaTime;

            if (_dirTimer > 5f)
            {
                _isAttack = true;
                _attackDir = dir;
            }

            dir = (_player.transform.position + Vector3.up * 1.5f - transform.position).normalized;
            Debug.Log(dir);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle - 180);
        }
    }
}
