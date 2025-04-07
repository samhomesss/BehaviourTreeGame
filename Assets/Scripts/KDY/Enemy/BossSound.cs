using UnityEngine;

public class BossSound : MonoBehaviour
{
    public void PlayBossSwordSound()
    {
        SoundManager.Instance.PlayBossSwordSound();
    }
    
    public void PlayKoongSound()
    {
        SoundManager.Instance.PlayKoongSound();
    }
    
    public void PlayDashAttackReadySound()
    {
        SoundManager.Instance.PlayDashAttackReadySound();
    }
    public void PlayDashAttackSound()
    {
        SoundManager.Instance.PlayDashAttackSound();
    }
}
