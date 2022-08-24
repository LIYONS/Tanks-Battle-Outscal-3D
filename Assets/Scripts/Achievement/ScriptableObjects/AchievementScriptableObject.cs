using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AchievementType
{
    RisingStorm,
    Veteran,
    WarLord
}

[CreateAssetMenu(fileName =" NewAchievement",menuName ="ScriptableObject/Achievement/AchievementObject")]
public class AchievementScriptableObject : ScriptableObject
{
    public AchievementType type;
    public string achievementName;
    public string achievementDescription;
}

    
