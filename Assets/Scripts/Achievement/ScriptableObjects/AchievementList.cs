using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = " AchievementList", menuName = "ScriptableObject/Achievement/AchievementList")]
public class AchievementList :ScriptableObject
{
    public List<AchievementScriptableObject> List;
}
