using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject target;

    public Vector3 offSet;

    public float smoothSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {
            transform.position = target.transform.position + offSet;

        }
    }
    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 desiredPosition = target.transform.position + offSet;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
