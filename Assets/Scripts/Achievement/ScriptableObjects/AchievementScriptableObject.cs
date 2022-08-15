using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =" NewAchievement",menuName ="ScriptableObject/Achievement/AchievementObject")]
public class AchievementScriptableObject : ScriptableObject
{ 
    public int achievementId;
    public string achievementName;
    public string achievementDescription;
    public bool isAchieved;
}

    
