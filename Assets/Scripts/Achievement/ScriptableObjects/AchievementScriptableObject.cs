using UnityEngine;

namespace TankGame.Achievements
{
    [CreateAssetMenu(fileName = " NewAchievement", menuName = "ScriptableObject/Achievement/AchievementObject")]
    public class AchievementScriptableObject : ScriptableObject
    {
        public AchievementType type;
        public string achievementName;
        public string achievementDescription;
    }

    public enum AchievementType
    {
        RisingStorm,
        Veteran,
        WarLord
    }
}

    
