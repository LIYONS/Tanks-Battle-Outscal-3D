using UnityEngine;

namespace TankGame.Tanks
{
    public enum TankType
    {
        None,
        Red,
        Green,
        Blue
    }
    [CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObject/TankScriptableObject")]
    public class TankScriptableObject : ScriptableObject
    {
        public TankType tankType;
        public Color tankColor;
        public float movementSpeed;
        public float turnSpeed;
        public float maxHealth = 100f;
        public float healthSliderTimer = 2f;
    }
}
