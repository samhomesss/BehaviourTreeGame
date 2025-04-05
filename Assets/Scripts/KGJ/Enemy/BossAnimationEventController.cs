using UnityEngine;

public class BossAnimationEventController : MonoBehaviour
{
    public bool IsDashing => _isDashing;
    bool _isDashing = false;
    PlayerStateManager _player;
    float _moveSpeed = 20f;
    float _dir;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerStateManager>();
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
}
