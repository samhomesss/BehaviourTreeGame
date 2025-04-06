using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int hpID; // �ش� ID�� ü���� �� ���� �� �� ����.
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
