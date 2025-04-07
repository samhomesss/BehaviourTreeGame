using System.Collections;
using UnityEngine;

/// <summary>
/// 보스 체력바 흔들리는 거 
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
        _originPos = RectTransform.anchoredPosition; // Canvas 기준 초기 위치 저장
        BossHpManager.BossHpDamageManager.OnPlayerAttackEvent += ShackingHP;
    }

    void OnDestroy()
    {
        BossHpManager.BossHpDamageManager.OnPlayerAttackEvent -= ShackingHP;
    }

    /// <summary>
    /// 보스가 공격받을 때 UI를 흔들어주는 메서드
    /// </summary>
    /// <param name="bossHP">현재 사용되지 않음</param>
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

        RectTransform.anchoredPosition = _originPos; // 원래 위치로 복귀
    }
}