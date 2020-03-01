using System;
using UnityEngine;
using System.Collections;
using Vuforia;

public class TargetTrack : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    private bool isTracked = false;
    
    public bool IsTracked { get { return isTracked; } }

    public event Action<bool> TrackStateChanged;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if (TrackStateChanged != null && !isTracked)
                TrackStateChanged.Invoke(isTracked = true);
        }
        else
        {
            if (TrackStateChanged != null && isTracked)
                TrackStateChanged.Invoke(isTracked = false);
        }
    }
}