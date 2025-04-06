using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossAttack"))
        {
            Debug.Log("보스공격성공");
            Attack attack = collision.gameObject.GetComponent<Attack>();
            PlayerHpManger.PlayerHpDamageEvent.PlayerDamaged(attack.attackDamage);
        }
    }
}
