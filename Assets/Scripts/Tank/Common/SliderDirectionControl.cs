using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Tanks
{
    public class SliderDirectionControl : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = transform.parent.rotation;
        }
    }
}
