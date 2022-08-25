using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ShellObject",menuName ="ScriptableObject/ShellObject")]
public class ShellObject : ScriptableObject
{
    public float maxChargeTime;
    public float minLaunchForce;
    public float maxLaunchForce;
    public float nextFireDelay;
    public float maxDamage;
    public float explosionRadius;
    public float maxLifeTime;
}
