using System.Collections;
using UnityEngine;

/// <summary>
/// ���� ü�¹� ��鸮�� �� 
/// </summary>
public class ShackingBossHP : MonoBehaviour
{
    protected RectTransform RectTransform;
    private Vector2 _originPos;

    private const float SHAKE_AMOUNT = 5f;
    private const float SHAKE_TIME = 0.2f;

    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        _originPos = RectTransform.anchoredPosition; // Canvas ���� �ʱ� ��ġ ����
        BossHpManager.BossHpDamageManager.OnPlayerAttackEvent += ShackingHP;
    }

    void OnDestroy()
    {
        BossHpManager.BossHpDamageManager.OnPlayerAttackEvent -= ShackingHP;
    }

    /// <summary>
    /// ������ ���ݹ��� �� UI�� �����ִ� �޼���
    /// </summary>
    /// <param name="bossHP">���� ������ ����</param>
    private void ShackingHP(int bossHP)
    {
        StartCoroutine(Shake(SHAKE_AMOUNT, SHAKE_TIME));
    }

    private IEnumerator Shake(float shakeAmount, float shakeTime)
    {
        float timer = 0f;
        while (timer <= shakeTime)
        {
            RectTransform.anchoredPosition = _originPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
            timer += Time.deltaTime;
            yield return null;
        }

        RectTransform.anchoredPosition = _originPos; // ���� ��ġ�� ����
    }
}