using UnityEngine;

public class PlayerDamageTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Todo: �ش� �κ��� ������ �÷��̾� �������� �ǰ� ������ ������ �� 
            PlayerHpManger.PlayerHpDamageEvent.PlayerDamaged(2); 
        }
    }
}
