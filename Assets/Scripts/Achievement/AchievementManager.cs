using UnityEngine;
using TMPro;
public class AchievementManager : MonoBehaviour
{
   
    [SerializeField] private AchievementList achievementList;
    [SerializeField] private UiHandler uiHandler;
   

    private void Start()
    {
        EventHandler.Instance.BulletAchievement += CheckForBulletAchievement;
    }
    private void CheckForBulletAchievement(int bulletCount)
    {
        AchievementScriptableObject achievementObject=null;
        if (achievementList.List !=null)
        {
            if(bulletCount==10)
            {
                achievementObject= UnlockAchievement(AchievementType.RisingStorm);
            }
            else if(bulletCount==25)
            {
                achievementObject = UnlockAchievement(AchievementType.Veteran);
            }
            else if(bulletCount==50)
            {
                achievementObject = UnlockAchievement(AchievementType.WarLord);
            }
        }
        if(achievementObject!=null && uiHandler)
        {
            uiHandler.OnAchievementUnlocked(achievementObject);
        }
    }
    private AchievementScriptableObject UnlockAchievement(AchievementType _type)
    {
        AchievementScriptableObject achievementObject = FindAchievementObject(_type);
        if (achievementObject)
        {
            achievementObject.isAchieved = true;
            return achievementObject;

        }
        return null;
    }
   
    private AchievementScriptableObject FindAchievementObject(AchievementType _type)
    {
        return achievementList.List.Find(ach => ach.type == _type);
    }
    
    private void OnDisable()
    {
        EventHandler.Instance.BulletAchievement -= CheckForBulletAchievement;
    }
}
