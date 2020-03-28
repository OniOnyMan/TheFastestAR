using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadFinishEvetHandler : MonoBehaviour
{
    public Transform _playerControllerTransform;

    void Start()
    {
        _playerControllerTransform = GetPlayerTransformRecursion(transform);
    }

    public void ReloadFinishEvent()
    {
        _playerControllerTransform.gameObject.GetComponent<PlayerController>().ReloadFinish();
    }

    private Transform GetPlayerTransformRecursion(Transform obj)
    {
        if (obj.tag == "PlayerOne" || obj.tag == "PlayerTwo")
        {
            return obj;
        }
        else return GetPlayerTransformRecursion(obj.parent);
    }
}
