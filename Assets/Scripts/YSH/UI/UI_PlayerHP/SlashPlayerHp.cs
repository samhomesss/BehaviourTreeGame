using UnityEngine;

public class SlashPlayerHp : MonoBehaviour
{
    public int hpID;
    void Start()
    {
        gameObject.SetActive(false);
        PlayerHpManger.PlayerHpDamageEvent.OnPlayerDamagedEvent += SlashPlayerHP;
    }

    void SlashPlayerHP(int playerHP)
    {
        if (playerHP + 1 == hpID)
        {
            gameObject.SetActive(true);
        }
    }
}
