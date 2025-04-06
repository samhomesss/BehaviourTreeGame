using UnityEngine;

public class ParallaxRepeatingManager : MonoBehaviour
{
    [System.Serializable]
    public class LayerSetting
    {
        public Sprite backgroundSprite;
        [SerializeField]public float parallaxFactor; // 깊이감 조절 1: 카메라와 동일속도 0: 정지 멀수록 1에 가깝게
        public int repeatCount; // 배경이 반복되는 개수 (최소 3개 이상 권장)
        public int order;
    }

    [SerializeField]
    private LayerSetting[] layers;

    private void Awake()
    {
        foreach (var layer in layers)
        {
            GameObject layerGO = new GameObject(layer.backgroundSprite.name + "_Layer");
            layerGO.transform.parent = transform;

            var repeatingLayer = layerGO.AddComponent<ParallaxRepeatingLayer>();
            repeatingLayer.SetBackgroundSetup(layer.backgroundSprite, layer.repeatCount, layer.parallaxFactor, layer.order);
        }
    }
}