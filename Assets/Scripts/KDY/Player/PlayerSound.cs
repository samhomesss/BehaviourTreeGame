using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void PlayerWalkSound()
    {
        SoundManager.Instance.PlayWalkSound();
    }
}
