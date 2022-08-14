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
    public Color tankColor;
    public float movementSpeed;
    public float turnSpeed;
    public float maxHealth = 100f;
    public float healthSliderTimer = 2f;
}
