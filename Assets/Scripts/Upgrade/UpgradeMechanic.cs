using System.Collections;
using System.Collections.Generic;
using TvZ.Management;
using TvZ.TimeMechanic;
using TvZ.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace TvZ.Upgrade
{
    public class UpgradeMechanic : MonoBehaviour
    {
        [SerializeField] UpgradeProgressionSO upgradeProgressionSO;
        [SerializeField] ResourcesStatSO resourcesStatSO;

        [SerializeField] UpgradeCard cardUpgradePrefab;
        [SerializeField] Transform cardLayoutTransform;

        private UpgradeLevelStatus upgradeLevelStatus;
        private TimeSystem timeSystem;


        private void Awake()
        {
            upgradeLevelStatus = GetComponent<UpgradeLevelStatus>();
        }

        private List<ResourcesEnum> upgradeResourcesList = new List<ResourcesEnum>();
        private List<UpgradeCard> upgradeCardSpawnedList = new List<UpgradeCard>();
        public void ShowUpgradeList()
        {

            if (upgradeResourcesList.Count > 0) return;
            

            upgradeResourcesList = upgradeProgressionSO.GetResourcesUpgradeList();

            foreach (ResourcesEnum item in upgradeResourcesList)
            {
                UpgradeCard spawnedCard = Instantiate(cardUpgradePrefab, cardLayoutTransform);

                SetUpgradeCard(item, spawnedCard, resourcesStatSO.GetImage(item));

                upgradeCardSpawnedList.Add(spawnedCard);


            }

        }

        public void UpdateCardDesc()
        {
            if (upgradeCardSpawnedList.Count < 0) return;

            foreach (UpgradeCard item in upgradeCardSpawnedList)
            {
                SetUpgradeCard(item.upgradeResourcesType, item, resourcesStatSO.GetImage(item.upgradeResourcesType));
            }
        }

        private void SetUpgradeCard(ResourcesEnum item, UpgradeCard spawnedCard, Sprite upgradeImage)
        {
            int levelStatus = upgradeLevelStatus.GetLevelResourcesNow(item);
            int maxLevel = upgradeProgressionSO.GetUpgradeMaxLevel(item);

            

            int nextLevel = levelStatus + 1;

            if(levelStatus == maxLevel)
            {
                spawnedCard.SetUpgradeComplete();
                return;
            }
           

            string costText = "Cost Gold : " + upgradeProgressionSO.GetUpgradeLevelCost(item, nextLevel).ToString();
            string cardDesc = upgradeProgressionSO.GetUpgradeDesc(item) + " " + upgradeProgressionSO.GetUpgradeLevelMultiplier(item, nextLevel).ToString("0.0");

            spawnedCard.SetUpgradeCard(item, cardDesc, costText,upgradeImage);

            spawnedCard.SetUpgradeButton(() => { UpgradeResources(spawnedCard, item, levelStatus); });
        }

        

       
        public void UpgradeResources(UpgradeCard cardUpgrade, ResourcesEnum resourcesEnum , int levelStatus)
        {

            int newLevel = levelStatus + 1;
            int dayUpgrade = 1;


            if (resourcesStatSO.GetResources(resourcesEnum) >= upgradeProgressionSO.GetUpgradeLevelCost(resourcesEnum,newLevel))
            {
                cardUpgrade.SetUpgradeStatus(true);
                timeSystem = FindAnyObjectByType<TimeSystem>();
                timeSystem.onDayChanged.AddListener(() => { UpgradeProgress(cardUpgrade, dayUpgrade, resourcesEnum, newLevel); });

            }
            else
            {
                print("Gold Tidak Cukup");

            }


        }

        public void UpgradeProgress(UpgradeCard cardUpgrade,int dayUpgrade, ResourcesEnum resourcesEnum, int newLevel)
        {
            dayUpgrade--;

            if(dayUpgrade == 0)
            {
                upgradeLevelStatus.SetLevelResources(resourcesEnum, newLevel);

                cardUpgrade.SetUpgradeStatus(false);

                

                
            }

        }

    }
}
