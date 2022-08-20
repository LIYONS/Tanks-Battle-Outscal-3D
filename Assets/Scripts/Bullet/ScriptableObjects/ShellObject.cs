using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="BulletObject",menuName ="ScriptableObject/BulletObject")]
public class ShellObject : ScriptableObject
{
    public TankType color;
    public float maxChargeTime;
    public float minLaunchForce;
    public float maxLaunchForce;
    public float nextFireDelay;
    public float maxDamage;
    public float explosionRadius;
    public float maxLifeTime;
}
