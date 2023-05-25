using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float yOffset = 0.3f;
    
    private Vector2 _myVelocity = Vector2.zero;

    private void Update()
    {
        Vector2 targetPosition = target.transform.TransformPoint(new Vector3(0, yOffset));

        if (targetPosition.y < transform.position.y)
        {
            return;
        }
        
        targetPosition = new Vector2(0,targetPosition.y);
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref _myVelocity,smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
