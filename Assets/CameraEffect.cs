using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public CinemachineImpulseSource _earthquake;
    public CinemachineImpulseSource _wave;
    public CinemachineImpulseSource _hit;

    private void Start()
    {
        PlayerHpManger.PlayerHpDamageEvent.OnEnemyAttackEvent += Hit;
    }

    public void Earthquake()
    {
        _earthquake.GenerateImpulse();
    }
    
    public void Wave()
    {
        _wave.GenerateImpulse();
    }
    
    public void Hit(int damage)
    {
        _hit.GenerateImpulse();
    }
}
