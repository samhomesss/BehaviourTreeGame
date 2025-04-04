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
        Debug.Log("°Ë±â »ý¼ºµÊ");

        Instantiate(_swordForcePrefab, (Vector2)transform.position + Vector2.up , Quaternion.identity);
    }
}
