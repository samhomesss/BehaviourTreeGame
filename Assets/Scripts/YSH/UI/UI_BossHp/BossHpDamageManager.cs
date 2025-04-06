using System;
using UnityEngine;

public class BossHpDamageManager
{
    public Action<int> OnEnemyDamagedEvent; // Damaged 보단 ChangeHp가 더 어울릴지도 
    public Action<int> OnPlayerAttackEvent; // 보통 때서 관리하는데 몇개 없으니까 여기서 하겠습니다.

    /// <summary>
    /// 그냥 1씩 데미지 받는거 
    /// </summary>
    /// <param name="playerHP"></param>
    public void BossDamaged(int damage)
    {
        OnPlayerAttackEvent?.Invoke(damage);
    }

    /// <summary>
    /// 그냥 피 빠졌을때 전체 다 불러서 상호 작용 시킬가
    /// </summary>
    /// <param name="playerHP"></param>
    public void ChangeHpReaction(int playerHP)
    {
        OnEnemyDamagedEvent?.Invoke(playerHP);
    }
}
