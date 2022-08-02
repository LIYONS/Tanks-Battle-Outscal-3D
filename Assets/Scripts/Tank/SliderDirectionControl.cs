using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderDirectionControl : MonoBehaviour
{
    private void Update()
    {
        transform.localRotation = transform.parent.localRotation;
    }
}
