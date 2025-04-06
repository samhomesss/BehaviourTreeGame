using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public enum CameraType
{
    Rasengan
    
}

public class CameraDirector : MonoBehaviour
{
    private PlayableDirector _pd; 
    
    public TimelineAsset[] timelines;
    
    private void Awake()
    {
        _pd = GetComponent<PlayableDirector>();
        if (_pd == null)
        {
            Debug.LogError("PlayableDirector component not found!");
        }
        
    }

    

    public void PlayTimeline(CameraType cameraType)
    {
        if (_pd == null) return;
        
        switch (cameraType)
        {
            case CameraType.Rasengan:
                _pd.Play(timelines[0]);
                break;
            default:
                Debug.LogError("Invalid camera type!");
                break;
        }
    }
    
    
    
    
    
    
    
    
    
    
}
