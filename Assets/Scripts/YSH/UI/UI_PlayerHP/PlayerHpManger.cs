using System.Collections;
using UnityEngine;

public class PlayerHpManger : MonoBehaviour
{
    #region PlayerHP Property
    /// <summary>
    /// 원래 따로 관리 해야 됩니다.
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

    public static PlayerHpDamageEvent PlayerHpDamageEvent => Instance._playerHpDamageEvent; // 플레이어가 데미지 입으면 바뀌는거 
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
