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
            if (attack == null)
            {
                Debug.LogError("�÷��̾� ���ݿ� Attack ��ũ��Ʈ�� �Ⱥپ�����");
                return;
            }
            Debug.Log("���� ���� : " + attack.attackName.ToString());
            BossHpManager.BossHpDamageManager.BossDamaged(attack.attackDamage);
        }
    }
}
