using UnityEngine;

public class BossDamageTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Todo: �ش� �κ��� ������ �÷��̾� �������� �ǰ� ������ ������ �� 
            BossHpManager.BossHpDamageManager.BossDamaged(2);
        }
    }
}
