using UnityEngine;

public class BossInstantiateShadow : MonoBehaviour
{
    GameObject _bossShadowPrefab;

    private void Start()
    {
        _bossShadowPrefab = Resources.Load<GameObject>("YSH/Object/Boss_Shadow");
    }

    public void InstantiateShadow(Vector2 pos)
    {
        //연막탄 소리
        SoundManager.Instance.PlaySmokeSound();
        GameObject go = Instantiate(_bossShadowPrefab, pos , Quaternion.identity);
    }
}
