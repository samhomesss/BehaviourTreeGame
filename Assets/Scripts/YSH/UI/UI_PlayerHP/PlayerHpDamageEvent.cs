using System;

public class PlayerHpDamageEvent 
{
    public Action<int> OnPlayerDamagedEvent; // Damaged ���� ChangeHp�� �� ��︱���� 
    public Action<int> OnEnemyAttackEvent; // ���� ���� �����ϴµ� � �����ϱ� ���⼭ �ϰڽ��ϴ�.

    /// <summary>
    /// �׳� 1�� ������ �޴°� 
    /// </summary>
    /// <param name="playerHP"></param>
    public void PlayerDamaged(int damage)
    {
        OnEnemyAttackEvent?.Invoke(damage);
    }

    /// <summary>
    /// �׳� �� �������� ��ü �� �ҷ��� ��ȣ �ۿ� ��ų��
    /// </summary>
    /// <param name="playerHP"></param>
    public void ChangeHpReaction(int playerHP)
    {
        OnPlayerDamagedEvent?.Invoke(playerHP);
    }
}
