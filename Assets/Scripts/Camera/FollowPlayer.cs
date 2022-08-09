using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject target;

    [SerializeField] private Vector3 offSet;

    [SerializeField] private float smoothSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {
            transform.position = target.transform.position + offSet;
        }
    }
    private void LateUpdate()
    {
        if (target)
        {
            Vector3 desiredPosition = target.transform.position + offSet;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
