using UnityEngine;

public class Attack : MonoBehaviour
{
    public string attackName;
    public int attackDamage;

    private GameObject _player;
    private GameObject _damagedEffect;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _damagedEffect = Resources.Load<GameObject>("HSC/Prefabs/Player/" + attackName);
    }

    public void ShowAttackEffect()
    {
        if (_player != null)
        {
            Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y + 2.5f);
            GameObject effect = Instantiate(_damagedEffect, spawnPos, Quaternion.identity);
            if (_player.transform.localScale.x < 0)
            {
                effect.transform.localScale = new Vector3(effect.transform.localScale.x * -1, effect.transform.localScale.y, effect.transform.localScale.z);
            }
            Destroy(effect, 0.3f);
        }
    }
}
