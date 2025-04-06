using UnityEngine;

public class BossHpManager : MonoBehaviour
{

    public int BossHp
    {
        get 
        {
            return _bossHp; 
        }
        set
        {
            _bossHp = value;
            //Todo : ü�� �ٲ������ �Ѱ� �ٷ��°�
            BossChangeHpReaction(_bossHp);
        }
    }

    int _bossHp = 150;

    public static BossHpManager Instance => _instance;
    static BossHpManager _instance;

    public static BossHpDamageManager BossHpDamageManager => Instance._bossHpDamageManager;
    BossHpDamageManager _bossHpDamageManager = new BossHpDamageManager();

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _bossHpDamageManager.OnPlayerAttackEvent += ChangeHp;
    }

    void ChangeHp(int bossDamaged)
    {

        BossHp -= bossDamaged;
        Debug.Log(BossHp + " ���� ��");
    }

    void BossChangeHpReaction(int bossHp)
    {
        _bossHpDamageManager.ChangeHpReaction(bossHp);
    }

}
