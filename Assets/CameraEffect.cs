using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;

public class CameraEffect : MonoBehaviour
{
    public CinemachineImpulseSource _earthquake;
    public CinemachineImpulseSource _wave;


    
    
    
    
    public void Earthquake()
    {
        _earthquake.GenerateImpulse();
    }
    
    public void Wave()
    {
        _wave.GenerateImpulse();
    }
}
