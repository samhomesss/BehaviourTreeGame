using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource actionAudioSource; // 단발 오디오 소스 (클릭, 피격음 등)

    [SerializeField] private AudioSource bgmAudioSource;
    
    [Tooltip("BGM")]
    [SerializeField] private AudioClip bgm;
    [Tooltip("BGM 볼륨")]
    [SerializeField] private float bgmVolume;
    
    [Header("플레이어 사운드")]
    [Tooltip("대쉬 사운드")]
    [SerializeField] private AudioClip dashSound;
    [Tooltip("대쉬 사운드 볼륨")]
    [SerializeField] private float dashSoundVolume;
    [Tooltip("검 휘두르는 사운드")]
    [SerializeField] private AudioClip[] swordSound;
    [Tooltip("검 휘두르는 사운드 볼륨")]
    [SerializeField] private float swordSoundVolume;
    [Tooltip("피격 사운드")]
    [SerializeField] private AudioClip hitSound;
    [Tooltip("피격 사운드 볼륨")]
    [SerializeField] private float hitSoundVolume;
    [Tooltip("강한 피격 사운드")]
    [SerializeField] private AudioClip powerHitSound;
    [Tooltip("강한 피격 사운드 볼륨")]
    [SerializeField] private float powerHitSoundVolume;
    [Tooltip("걷기 사운드")]
    [SerializeField] private AudioClip walkSound;
    [Tooltip("걷기 사운드 볼륨")]
    [SerializeField] private float walkSoundVolume;
    
    [Header("보스 사운드")]
    [Tooltip("보스 칼질 사운드")]
    [SerializeField] private AudioClip bossSwordSound;
    [Tooltip("보스 칼질 사운드 볼륨")]
    [SerializeField] private float bossSwordSoundVolume;
    [Tooltip("표창 사운드")]
    [SerializeField] private AudioClip kunaiSound;
    [Tooltip("표창 사운드 볼륨")]
    [SerializeField] private float kunaiSoundVolume;
    [Tooltip("나선환 사운드")]
    [SerializeField] private AudioClip rasenganSound;
    [Tooltip("나선환 사운드 볼륨")]
    [SerializeField] private float rasenganSoundVolume;
    [Tooltip("기합 사운드")]
    [SerializeField] private AudioClip shoutSound;
    [Tooltip("기합 사운드 볼륨")]
    [SerializeField] private float shoutSoundVolume;
    [Tooltip("연막탄 사운드")]
    [SerializeField] private AudioClip smokeSound;
    [Tooltip("연막탄 사운드 볼륨")]
    [SerializeField] private float smokeSoundVolume;
    

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
        PlayBGMSound(bgm, bgmVolume);
    }
    
    public void PlayBGMSound(AudioClip clip, float volume = 1f)
    {
        if (clip != null && bgmAudioSource != null)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.volume = volume;
            bgmAudioSource.Play();
        }
    }

    public void PlayDashSound()
    {
        PlaySound(dashSound, dashSoundVolume);
    }
    public void PlaySwordSound(int num)
    {
        PlaySound(swordSound[num], swordSoundVolume);
    }

    public void PlayKunaiSound()
    {
        PlaySound(kunaiSound, kunaiSoundVolume);
    }

    public void PlayRasenganSound()
    {
        PlaySound(rasenganSound, rasenganSoundVolume);
    }

    public void PlayShoutSound()
    {
        PlaySound(shoutSound, shoutSoundVolume);
    }

    public void PlayHitSound()
    {
        PlaySound(hitSound, hitSoundVolume);
    }

    public void PlayPowerHitSound()
    {
        PlaySound(powerHitSound, powerHitSoundVolume);
    }

    public void PlayWalkSound()
    {
        PlaySound(walkSound, walkSoundVolume);
    }

    public void PlaySmokeSound()
    {
        PlaySound(smokeSound, smokeSoundVolume);
    }

    public void PlayBossSwordSound()
    {
        PlaySound(bossSwordSound, bossSwordSoundVolume);
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
