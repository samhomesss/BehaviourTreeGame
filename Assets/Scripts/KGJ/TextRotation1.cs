using UnityEngine;

public class TextRotation1 : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float speed = 1f; // 색상 변화 속도

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 시간에 따라 Hue 값을 0~1 사이로 순환
        float hue = (Time.time * speed) % 1f;
        // HSB에서 RGB로 변환
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        _spriteRenderer.color = rainbowColor;
    }
}