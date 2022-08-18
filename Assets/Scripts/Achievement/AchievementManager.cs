using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AchievementManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private float uiTimer;
    [SerializeField] private AchievementList achievementList;

    private const string defaultAchievementText = "Achievement Unlocked : ";

    private void OnEnable()
    {
        PlayerBulletMovement.BulletAchievement += CheckForBulletAchievement;
    }
    private void Start()
    {
        achievementPanel.SetActive(false);
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
        if(achievementObject!=null)
        {
            ShowAchievementUi(achievementObject);
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
    private void ShowAchievementUi(AchievementScriptableObject achievement)
    {
        achievementText.text = defaultAchievementText + achievement.name;
        achievementDescription.text = achievement.achievementDescription;
        achievementPanel.SetActive(true);
        Invoke(nameof(DeactivateUi), uiTimer);
    }
    private AchievementScriptableObject FindAchievementObject(AchievementType _type)
    {
        return achievementList.List.Find(ach => ach.type == _type);
    }
    private void DeactivateUi()
    {
        achievementPanel.SetActive(false);
    }
    private void OnDisable()
    {
        PlayerBulletMovement.BulletAchievement -= CheckForBulletAchievement;
    }
}
