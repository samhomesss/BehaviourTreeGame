using UnityEngine;

public class BossCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // im si ro man den layer name
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            Debug.Log("플레이어공격성공");
            Attack attack = collision.gameObject.GetComponent<Attack>();
            if (attack == null)
            {
                Debug.LogError("플레이어 공격에 Attack 스크립트가 안붙어있음");
                return;
            }
            Debug.Log("받은 공격 : " + attack.attackName.ToString());
            BossHpManager.BossHpDamageManager.BossDamaged(attack.attackDamage);
        }
    }
}
