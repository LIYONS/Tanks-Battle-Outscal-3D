using UnityEngine;

[CreateAssetMenu(fileName ="TankScriptableObject",menuName ="ScriptableObject/TankScriptableObject")]
public class TankScriptableObject : ScriptableObject
{
    public TankType tankType;
    [Range(.5f,2f)]
    public float size;
    //public float health;
    public float movementSpeed;
    public float turnSpeed;
    //public float damage;
}
