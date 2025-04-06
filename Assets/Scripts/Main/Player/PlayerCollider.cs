using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossAttack"))
        {
            Debug.Log("�������ݼ���");
            Attack attack = collision.gameObject.GetComponent<Attack>();
            PlayerHpManger.PlayerHpDamageEvent.PlayerDamaged(attack.attackDamage);
        }
    }
}
