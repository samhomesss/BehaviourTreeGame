using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int bossHpID;
    GameObject _flowerEffect;
    GameObject _flowerLeefEffect;
    bool _isDestroy = false;
    void Start()
    {
        _flowerEffect = Resources.Load<GameObject>("YSH/Effect/FlowerParticle");
        _flowerLeefEffect = Resources.Load<GameObject>("YSH/Effect/FlowerLeefParticle");
        BossHpManager.BossHpDamageManager.OnEnemyDamagedEvent += ChangeBossHPBar;
    }

    void ChangeBossHPBar(int bossHP)
    {
        if (((bossHP) / 10) == bossHpID && !_isDestroy)
        {
            GameObject go = Instantiate(_flowerLeefEffect, transform.position, Quaternion.identity);
            Destroy(go, 3f);
        }
        //Todo : ���Ŀ� ���� 10 ������ �̻� ���°� �÷��̾� �ȿ� �ִٸ� ���� �ؾ� �� �ڵ� 
        // ����� �ѹ� �ۿ� Ȯ�� ���� ���� ũ�� ���� �ڿ� ���� ������ ��?
        if (((bossHP) / 10) + 1 == bossHpID && !_isDestroy)
        {
            GameObject go = Instantiate(_flowerEffect, transform.position, Quaternion.identity);
            _isDestroy = true;
            Destroy(go, 3f);
            Destroy(gameObject);
        }
    }
}
