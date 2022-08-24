using UnityEngine;
using TMPro;

public enum AchievementStatus
{
    Locked,
    Unlocked
}
public class AchievementManager : MonoBehaviour
{
   
    [SerializeField] private AchievementList achievementList;
    [SerializeField] private InGameUiHandler uiHandler;
   

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
                achievementObject = UnlockAchievement(AchievementType.RisingStorm);
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
        if (achievementObject &&  ! IsUnlocked(achievementObject))
        {
            PlayerPrefs.SetInt(achievementObject.name, (int)AchievementStatus.Unlocked);
            return achievementObject;
        }
        return null;
    }

    private bool IsUnlocked(AchievementScriptableObject achievementObject)
    {
        if(PlayerPrefs.HasKey(achievementObject.name))
        {
            if(PlayerPrefs.GetInt(achievementObject.name)==(int)AchievementStatus.Unlocked)
            {
                return true;
            }
            return false;
        }
        return false;
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
