using UnityEngine;

public enum TankColor
{
    None,
    Red,
    Green,
    Blue
}
[CreateAssetMenu(fileName ="TankScriptableObject",menuName ="ScriptableObject/TankScriptableObject")]
public class TankScriptableObject : ScriptableObject
{
    public Color tankTurretColor;
    public Color tankChassisColor;
    [Range(.5f,2f)]
    public float size;
    public float movementSpeed;
    public float turnSpeed;
    public float maxHealth = 100f;
    public float healthSliderTimer = 2f;
}
