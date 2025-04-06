using System;
using UnityEngine;

public class BossHpDamageManager
{
    public Action<int> OnEnemyDamagedEvent; // Damaged ���� ChangeHp�� �� ��︱���� 
    public Action<int> OnPlayerAttackEvent; // ���� ���� �����ϴµ� � �����ϱ� ���⼭ �ϰڽ��ϴ�.

    /// <summary>
    /// �׳� 1�� ������ �޴°� 
    /// </summary>
    /// <param name="playerHP"></param>
    public void BossDamaged(int damage)
    {
        OnPlayerAttackEvent?.Invoke(damage);
    }

    /// <summary>
    /// �׳� �� �������� ��ü �� �ҷ��� ��ȣ �ۿ� ��ų��
    /// </summary>
    /// <param name="playerHP"></param>
    public void ChangeHpReaction(int playerHP)
    {
        OnEnemyDamagedEvent?.Invoke(playerHP);
    }
}
