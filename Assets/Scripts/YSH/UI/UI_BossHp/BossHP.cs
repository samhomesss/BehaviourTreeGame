using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int bossHpID;
    GameObject _flowerEffect;
    bool _isDestroy = false;
    void Start()
    {
        _flowerEffect = Resources.Load<GameObject>("YSH/Effect/FlowerParticle");
        BossHpManager.BossHpDamageManager.OnEnemyDamagedEvent += ChangeBossHPBar;
    }

    void ChangeBossHPBar(int bossHP)
    {
        Debug.Log(((int)(bossHP) / 10) + 1);

        if (((bossHP) / 10) + 1 == bossHpID && !_isDestroy)
        {
            GameObject go = Instantiate(_flowerEffect, transform.position, Quaternion.identity);
            _isDestroy = true;
            Destroy(go, 3f);
            Destroy(gameObject);
        }
    }
}
