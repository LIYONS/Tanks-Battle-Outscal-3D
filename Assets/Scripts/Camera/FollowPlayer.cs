using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform target;

    [SerializeField] private Vector3 positionOffset;

    [SerializeField] private float smoothSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (target)
        {
            Vector3 desiredPosition = target.transform.position + positionOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
