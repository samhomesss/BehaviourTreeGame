using UnityEngine;

public class BossCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // im si ro man den layer name
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            Debug.Log("�÷��̾���ݼ���");
            Attack attack = collision.gameObject.GetComponent<Attack>();
            BossHpManager.BossHpDamageManager.BossDamaged(attack.attackDamage);
        }
    }
}
