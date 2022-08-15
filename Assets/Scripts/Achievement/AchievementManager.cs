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
    private int  bulletCount;

    private void OnEnable()
    {
        BulletMovement.OnBulletFired += UpdateBulletCount;
    }
    private void Start()
    {
        achievementPanel.SetActive(false);
        bulletCount = 0;
    }

    private void UpdateBulletCount()
    {
        bulletCount++;
        CheckForAchievement();
    }

    private void CheckForAchievement()
    {
        if(bulletCount==10 && !FindAchievementObject(AchievementType.RisingStorm).isAchieved)
        {
            ShowAchievementUi(FindAchievementObject(AchievementType.RisingStorm));
            return;
        }

        else if(bulletCount==25 && !FindAchievementObject(AchievementType.Veteran).isAchieved)
        {
            ShowAchievementUi(FindAchievementObject(AchievementType.Veteran));
            return;
        }
        else if (bulletCount == 25 && !FindAchievementObject(AchievementType.WarLord).isAchieved)
        {
            ShowAchievementUi(FindAchievementObject(AchievementType.WarLord));
            return;
        }
    }
    private void ShowAchievementUi(AchievementScriptableObject achievement)
    {
        achievementText.text = defaultAchievementText + achievement.name;
        achievementDescription.text = achievement.achievementDescription;
        achievementPanel.SetActive(true);
        Invoke("DeactivateUi", uiTimer);
    }
    private AchievementScriptableObject FindAchievementObject(AchievementType _type)
    {
        for(int i=0;i<achievementList.List.Count;i++)
        {
            if(achievementList.List[i].type==_type)
            {
                return achievementList.List[i].achievementObject;
            }
        }
        return null;
    }
    private void DeactivateUi()
    {
        achievementPanel.SetActive(false);
    }
    private void OnDisable()
    {
        BulletMovement.OnBulletFired -= UpdateBulletCount;
    }
}
