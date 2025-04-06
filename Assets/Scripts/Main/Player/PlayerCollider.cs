using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossAttack"))
        {
            Debug.Log("�������ݼ���");
            Attack attack = collision.gameObject.GetComponent<Attack>();
            if (attack == null)
            {
                Debug.LogError("���� ���ݿ� Attack ��ũ��Ʈ�� �Ⱥپ�����");
                return;
            }
            Debug.Log("���� ���� : " + attack.attackName.ToString());
            PlayerHpManger.PlayerHpDamageEvent.PlayerDamaged(attack.attackDamage);
        }
    }
}
