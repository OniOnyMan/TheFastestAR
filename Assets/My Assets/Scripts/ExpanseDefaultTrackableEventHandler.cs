using System;
using UnityEngine;
using System.Collections;
using Vuforia;

public class ExpanseDefaultTrackableEventHandler : DefaultTrackableEventHandler
{
    private bool isTracked = false;
    
    public event Action<bool, GameObject> TrackStateChanged;
    public bool IsEnabled = true;

    public bool IsTracked { get { return isTracked; } }
    
    public new void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if (TrackStateChanged != null && !isTracked)
                TrackStateChanged.Invoke(isTracked = true, gameObject);
            if (IsEnabled) OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            if (TrackStateChanged != null && isTracked)
                TrackStateChanged.Invoke(isTracked = false, gameObject);
            OnTrackingLost();
        }
        else OnTrackingLost();
    }
}