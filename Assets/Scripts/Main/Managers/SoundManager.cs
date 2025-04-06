using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource actionAudioSource; // �ܹ� ����� �ҽ� (Ŭ��, �ǰ��� ��)

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
        // TODO : stopsound �����Ŵ� �׼� �����ϴ°� �����ϴ�.
        // �׼� += StopSound();
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
        Debug.Log("���� ����");
        actionAudioSource.Stop();
    }
}
