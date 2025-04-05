using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector pd;
    public TimelineAsset timelineAsset; // 타임라인 에셋


    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void PlayTimeline()
    {
        pd.Stop();
        pd.Play(timelineAsset);
    }
}
