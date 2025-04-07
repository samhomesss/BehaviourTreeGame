using System.Collections;
using UnityEngine;

public class PlayerHpManger : MonoBehaviour
{
    #region PlayerHP Property
    /// <summary>
    /// ���� ���� ���� �ؾ� �˴ϴ�.
    /// </summary>
    public int PlayerHP
    {
        get
        {
            return _playerHp;
        }
        set
        {
            _playerHp = value;
            ChangePlayerHpReaction(_playerHp);
        }
    }
    int _playerHp = 12;
    #endregion

    #region Managers & Action
    public static PlayerHpManger Instance => _instance;
    static PlayerHpManger _instance;

    public static PlayerHpDamageEvent PlayerHpDamageEvent => Instance._playerHpDamageEvent; // �÷��̾ ������ ������ �ٲ�°� 
    PlayerHpDamageEvent _playerHpDamageEvent = new PlayerHpDamageEvent();
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _playerHpDamageEvent.OnEnemyAttackEvent += ChangHP;
    }

    void ChangHP(int damage)
    {
        // 피격음
        SoundManager.Instance.PlayPlayerHitSound();        
        StartCoroutine(PlayerHPChanagedTimer(damage));
    }

    void ChangePlayerHpReaction(int playerHP)
    {
        _playerHpDamageEvent.ChangeHpReaction(playerHP);
    }

    IEnumerator PlayerHPChanagedTimer(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            PlayerHP--;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
