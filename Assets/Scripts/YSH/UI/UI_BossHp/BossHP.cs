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
        //Todo : 추후에 만약 10 데미지 이상 들어가는게 플레이어 안에 있다면 수정 해야 할 코드 
        // 현재는 한번 밖에 확인 안함 값이 크게 들어가면 뒤에 꺼만 조절할 듯?
        if (((bossHP) / 10) + 1 == bossHpID && !_isDestroy)
        {
            GameObject go = Instantiate(_flowerEffect, transform.position, Quaternion.identity);
            _isDestroy = true;
            Destroy(go, 3f);
            Destroy(gameObject);
        }
    }
}
