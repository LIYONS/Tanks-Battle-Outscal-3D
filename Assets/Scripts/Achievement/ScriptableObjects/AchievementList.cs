using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AchievementType
{
    RisingStorm,
    Veteran,
    WarLord
}

[Serializable]
public class Achievement
{
    public AchievementType type;
    public AchievementScriptableObject achievementObject;
}

[CreateAssetMenu(fileName = " AchievementList", menuName = "ScriptableObject/Achievement/AchievementList")]
public class AchievementList :ScriptableObject
{
    public List<Achievement> List;
}
