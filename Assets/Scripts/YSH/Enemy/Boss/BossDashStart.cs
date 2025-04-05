using UnityEngine;

public class BossDashStart : MonoBehaviour
{
    Vector2 dir;
    PlayerStateManager _player;
    const float ATTACK_SPEED = 50f;
    float _timer = 0;

    void Start()
    {
        dir = (_player.transform.position + Vector3.up * 1.5f - transform.position).normalized;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        transform.Translate(dir * ATTACK_SPEED * Time.deltaTime);
        if (_timer > 3f)
        {
            _timer = 0;
            this.enabled = false;
        }

    }
}
