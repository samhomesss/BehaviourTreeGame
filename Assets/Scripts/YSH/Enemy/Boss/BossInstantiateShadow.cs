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
        GameObject go = Instantiate(_bossShadowPrefab, pos , Quaternion.identity);
    }
}
