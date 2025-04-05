using UnityEngine;

public class BossAnimationEventController : MonoBehaviour
{
    bool _isDashing = false;
    PlayerStateManager _player;
    float _moveSpeed = 20f;
    float _dir;
    PolygonCollider2D[] _colliders;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerStateManager>();
        _colliders = FindObjectsByType<PolygonCollider2D>(FindObjectsSortMode.None);
        foreach (var collider in _colliders)
        {
            Debug.Log(collider.ToString());
        }
    }

    private void Update()
    {
        _dir = _player.transform.position.x > transform.position.x ? 1 : -1;
        if (_isDashing)
        {
            transform.Translate(_dir * Vector3.right * _moveSpeed * Time.deltaTime);
        }
    }

    public void GetAttackRhythmDashStart()
    {
        _isDashing = true;
    }

    public void GetAttackRhythmDashEnd()
    {
        _isDashing = false;
    }

    public void GetAttack1Start()
    {
        _colliders[0].enabled = true;
    }

    public void GetAttack1End()
    {
        _colliders[0].enabled = false;
    }

    public void GetAttack2Start()
    {
        _colliders[1].enabled = true;
    }

    public void GetAttack2End()
    {
        _colliders[1].enabled = false;
    }

    public void GetAttack3Start()
    {
        _colliders[2].enabled = true;
    }

    public void GetAttack3End()
    {
        _colliders[2].enabled = false;
    }

    public void GetAttackAirStart()
    {
        _colliders[3].enabled = true;
    }

    public void GetAttackAirEnd()
    {
        _colliders[3].enabled = false;
    }

    public void GetAttackSpecialStart()
    {
        _colliders[4].enabled = true;
    }

    public void GetAttackSpecialEnd()
    {
        _colliders[4].enabled = false;
    }

    public void GetAttackDownStart()
    {
        _colliders[5].enabled = true;
    }

    public void GetAttackDownEnd()
    {
        _colliders[5].enabled = false;
    }
}
