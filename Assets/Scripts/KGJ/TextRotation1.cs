using UnityEngine;

public class TextRotation1 : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float speed = 1f; // ���� ��ȭ �ӵ�

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �ð��� ���� Hue ���� 0~1 ���̷� ��ȯ
        float hue = (Time.time * speed) % 1f;
        // HSB���� RGB�� ��ȯ
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        _spriteRenderer.color = rainbowColor;
    }
}