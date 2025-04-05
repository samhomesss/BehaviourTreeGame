using UnityEngine;

public class BossInstanctiateSwordForce : MonoBehaviour
{
    GameObject _swordForcePrefab;

    private void Start()
    {
        _swordForcePrefab = Resources.Load<GameObject>("YSH/Object/SwordForce");
    }

    public void InstantiateSword()
    {
        GameObject go = Instantiate(_swordForcePrefab, (Vector2)transform.position + Vector2.up , Quaternion.identity);
        Destroy(go, 4f);
    }
}
