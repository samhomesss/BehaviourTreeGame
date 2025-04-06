using UnityEngine;
using System.Collections.Generic;

public class ParallaxRepeatingLayer : MonoBehaviour
{
    [Header("배경 설정")]
    [SerializeField]
    private Sprite backgroundsSprite; // 반복할 배경 스프라이트 프리팹
    [SerializeField] 
    private int repeatCount; // 배경이 반복되는 개수 (최소 3개 이상 권장)
    [SerializeField, Range(0f, 1f)]
    private float parallaxFactor; // 이동 속도 계수 (깊이감 조절)
    [SerializeField]
    private int sortingOrder; // 정렬 순서 설정

    private Transform cameraTransform;
    private Vector3 previousCameraPosition; // 카메라의 이전 위치

    private float spriteWidth;
    private List<Transform> backgroundInstances = new List<Transform>();

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void InitializeBackgrounds()
    {
        // 배경 스프라이트 너비 확인(x 스케일 2배로 쓸거라서)
        spriteWidth = backgroundsSprite.bounds.size.x * 2;

        // 배경 스프라이트를 반복 생성 및 배치
        for (int i = -repeatCount/2; i <= repeatCount/2; i++)
        {
            GameObject backgroundInstance = new GameObject("Background_" + i);
            backgroundInstance.transform.parent = transform;
            backgroundInstance.transform.localScale = new Vector3(2,1,1);

            SpriteRenderer spriteRenderer = backgroundInstance.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = backgroundsSprite;
            spriteRenderer.sortingOrder = sortingOrder; // 정렬 순서 설정
            spriteRenderer.color = new Color(94f / 255f, 94f / 255f, 94f / 255f, 1f);

            // 배경 위치 설정
            Vector3 position = new Vector3(i * spriteWidth, 8, 0);
            backgroundInstance.transform.localPosition = position;

            backgroundInstances.Add(backgroundInstance.transform);
        }
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - previousCameraPosition;

        // 패럴랙스 효과 적용
        Vector3 parallaxMovement = new Vector3(cameraDelta.x * parallaxFactor, 0, 0);
        transform.position += parallaxMovement;

        previousCameraPosition = cameraTransform.position;

    }
    
    public void SetBackgroundSetup(Sprite sprite, int count, float factor, int order)
    {
        backgroundsSprite = sprite;
        repeatCount = count;
        parallaxFactor = factor;
        sortingOrder = order;

        // 기존 배경 인스턴스 제거
        foreach (var instance in backgroundInstances)
        {
            Destroy(instance.gameObject);
        }
        backgroundInstances.Clear();

        // 새로운 배경 초기화
        InitializeBackgrounds();
    }
}
