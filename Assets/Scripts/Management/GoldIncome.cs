using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Upgrade;

namespace TvZ.Management
{
    public class GoldIncome : MonoBehaviour
    {
        [SerializeField] ResourcesStatSO resourcesStatSO;
        [SerializeField] UpgradeProgressionSO progressionSO;
        [SerializeField] float incomePerDay = 50;

        [SerializeField] float blueprintGained = 1;

        UpgradeLevelStatus upgradeLevelStatus;

        private void Start()
        {
            upgradeLevelStatus = FindAnyObjectByType<UpgradeLevelStatus>();
        }
        public void GainGoldIncome()
        {
            float multiplierAmount = progressionSO.GetUpgradeLevelMultiplier(ResourcesEnum.Gold, upgradeLevelStatus.GetLevelResourcesNow(ResourcesEnum.Gold));

            print("Gold Multiplier = " + multiplierAmount + "x ");

            resourcesStatSO.AddResources(ResourcesEnum.Gold,incomePerDay * multiplierAmount);
        }

        public void GainBluePrint()
        {
            resourcesStatSO.AddResources(ResourcesEnum.BluePrint, blueprintGained);
        }


    }
}
