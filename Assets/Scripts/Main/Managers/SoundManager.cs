using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource actionAudioSource; // 단발 오디오 소스 (클릭, 피격음 등)

    [SerializeField] private AudioSource bgmAudioSource;

    [SerializeField] private AudioClip basicClickSound;
    [SerializeField] private float basicClickSoundVolume;
    [SerializeField] private AudioClip damagedSound;
    [SerializeField] private float damagedSoundVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // TODO : stopsound 같은거는 액션 구독하는게 좋습니다.
        // 액션 += StopSound();
    }

    public void PlayBasicClickSound()
    {
        PlaySound(basicClickSound, basicClickSoundVolume);
    }

    public void PlayAircraftMoveSound()
    {
        PlaySound(damagedSound, damagedSoundVolume);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && actionAudioSource != null)
        {
            actionAudioSource.PlayOneShot(clip, volume);
        }
    }

    private void StopSound()
    {
        Debug.Log("사운드 정지");
        actionAudioSource.Stop();
    }
}
