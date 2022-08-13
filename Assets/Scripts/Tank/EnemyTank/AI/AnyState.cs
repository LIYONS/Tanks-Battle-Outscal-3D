using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyState : TankState
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            //Debug.Log(Vector3.Distance(transform.position, other.transform.position));
        }
    }
}
