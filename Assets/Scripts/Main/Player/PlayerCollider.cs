using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossAttack"))
        {
            Debug.Log("보스공격성공");
            Attack attack = collision.gameObject.GetComponent<Attack>();
            if (attack == null)
            {
                Debug.LogError("보스 공격에 Attack 스크립트가 안붙어있음");
                return;
            }
            Debug.Log("받은 공격 : " + attack.attackName.ToString());
            PlayerHpManger.PlayerHpDamageEvent.PlayerDamaged(attack.attackDamage);
        }
    }
}
