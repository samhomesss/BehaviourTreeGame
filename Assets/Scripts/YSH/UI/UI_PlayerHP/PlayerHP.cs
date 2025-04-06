using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int hpID; // 해당 ID로 체력이 몇 인지 알 수 있음.
    GameObject _bloodSplahEffect;

    private void Start()
    {
        _bloodSplahEffect = Resources.Load<GameObject>("YSH/Effect/Blood_Splash");
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += ChangePlayerHPBar;  
    }

    void ChangePlayerHPBar(int playerHP)
    {
        if (playerHP + 1 == hpID)
        {
            GameObject go = Instantiate(_bloodSplahEffect, transform.position, Quaternion.identity);
            Destroy(go, 3f);
            Destroy(gameObject);
        }
    }
}
