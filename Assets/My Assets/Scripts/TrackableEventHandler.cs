using System;
using UnityEngine;
using System.Collections;
using Vuforia;

public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public event Action<bool, GameObject> TrackStateChanged;
    public bool IsEnabled = true;

    public bool IsTracked { get { return isTracked; } }

    private TrackableBehaviour mTrackableBehaviour;
    private bool isTracked = false;

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
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            if (TrackStateChanged != null && !isTracked)
                TrackStateChanged.Invoke(isTracked = true, gameObject);
            if (IsEnabled)
                EnableComponents(true);
        }
        else if(previousStatus == TrackableBehaviour.Status.TRACKED &&
               newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            if (TrackStateChanged != null && isTracked)
                TrackStateChanged.Invoke(isTracked = false, gameObject);
            EnableComponents(false);
        }
        else EnableComponents(false);
    }

    public void EnableComponents(bool condition)
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        foreach (var component in rendererComponents)
            component.enabled = condition;
        foreach (var component in colliderComponents)
            component.enabled = condition;
        foreach (var component in canvasComponents)
            component.enabled = condition;
    }
}