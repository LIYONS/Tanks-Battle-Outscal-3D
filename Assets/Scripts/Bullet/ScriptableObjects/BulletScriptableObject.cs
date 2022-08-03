using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BulletObject",menuName ="ScriptableObject/BulletObject")]
public class BulletScriptableObject : ScriptableObject
{
    public BulletType bulletType;
    public float speed;
}
