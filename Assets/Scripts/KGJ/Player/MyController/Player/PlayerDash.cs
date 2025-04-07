using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    bool _isDashing = false;
    bool _haveDashChance = true;

    Vector2 _moveDir;
    float _dashStartTimer;
    float _dashEndTimer;

    float _dashStartDuration = 0.15f;
    float _dashEndDuration = 0.03f;

    float _dashSpeed = 40f;
    float _dashEndSpeed = 15f;

    float _originalGravity;

    float _dashCooldown = 0.5f;

    GameObject _shadowPrefab;
    float _shadowLifetime = 0.1f;
    float _shadowInterval = 0.05f;

    void Start()
    {
        Managers.InputManager.OnDashEvent += Dash;
        _originalGravity = PlayerStateManager.PlayerRigid.gravityScale;
        _shadowPrefab = Resources.Load<GameObject>("KGJ/Prefabs/Player/PlayerDash_Prefabs");
    }

    void Dash()
    {
        if (!_isDashing && _haveDashChance)
        {
            SoundManager.Instance.PlayDashSound();
            PlayerStateManager.IsDashing = true;
            _isDashing = true; 
            _dashStartTimer = _dashStartDuration;
            _dashEndTimer = _dashEndDuration;

            //StartCoroutine(PauseForOneFrame());
            StartCoroutine(DashAction());
        }
    }

    IEnumerator DashAction()
    {
        _moveDir = Managers.InputManager.MoveVec;

        if (_moveDir.x == 0) // ����Ű �Է��� ���ٸ�
        {
            if (PlayerStateManager.FilpX) // ������
            {
                _moveDir.x = -1;
            }
            else // ����
            {
                _moveDir.x = 1;
            }
        }

        _moveDir = _moveDir.normalized;

        StartCoroutine(SpawnShadows());

        // ��� ���� �ܰ�
        while (_dashStartTimer > 0f)
        {
            _dashStartTimer -= Time.deltaTime; // Ÿ�̸� ����
            PlayerStateManager.PlayerRigid.gravityScale = 0f;
            PlayerStateManager.PlayerRigid.linearVelocity = _moveDir * _dashSpeed;
            yield return null;
        }

        // ��� ���� �ܰ�
        while (_dashEndTimer > 0f)
        {
            _dashEndTimer -= Time.deltaTime; // Ÿ�̸� ����
            PlayerStateManager.PlayerRigid.gravityScale = _originalGravity;
            PlayerStateManager.PlayerRigid.linearVelocity = _moveDir * _dashEndSpeed;
            yield return null;
        }

        // ��� �Ϸ� ��
        PlayerStateManager.IsDashing = false;
        _isDashing = false;
        _haveDashChance = false;

        StartCoroutine(ResetDashChance());
    }

    IEnumerator PauseForOneFrame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1f;
    }

    IEnumerator ResetDashChance()
    {
        yield return new WaitForSeconds(_dashCooldown);
        _haveDashChance = true;
    }

    IEnumerator SpawnShadows()
    {
        while (_isDashing)
        {
            GameObject shadow = Instantiate(_shadowPrefab, transform.position, transform.rotation);
            if (PlayerStateManager.FilpX)
                shadow.transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                shadow.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(shadow, _shadowLifetime);
            yield return new WaitForSeconds(_shadowInterval);
        }
    }

    private void OnDisable()
    {
        Managers.InputManager.OnDashEvent -= Dash;
    }
}