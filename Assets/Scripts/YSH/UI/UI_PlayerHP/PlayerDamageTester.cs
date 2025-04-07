using UnityEngine;

public class PlayerDamageTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Todo: 해당 부분을 보스가 플레이어 때렸을때 피격 판정에 넣으면 됨 
            PlayerHpManger.PlayerHpDamageEvent.PlayerDamaged(2); 
        }
    }
}
