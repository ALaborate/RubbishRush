using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScreener : MonoBehaviour//screens translation of target to gameObject
{
    public Transform target;

    private Vector3 difference;
    void Start()
    {
        if (target != null)
            difference = gameObject.transform.position - target.position;
        else
            Debug.LogError("target reference is not set to an instance of any object!");
    }

    public void LateUpdate()
    {
        if (target != null)
            gameObject.transform.position = target.position + difference;
    }

}
