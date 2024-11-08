using System.Collections;
using System.Collections.Generic;
using TvZ.Management;
using UnityEngine;
using UnityEngine.UI;

namespace TvZ.Upgrade
{
    [CreateAssetMenu(fileName = "ListResearchSO", menuName = "Create Asset/ Upgrade/ Create Upgrade Progression")]
    public class UpgradeProgressionSO : ScriptableObject
    {
        public UpgradeResources[] upgradeResources;


        private Dictionary<ResourcesEnum, Dictionary<int, float>> resourcesUpgradeMultiplierDict;
        private Dictionary<ResourcesEnum, Dictionary<int, float>> resourcesUpgradeCostDict;
        private Dictionary<ResourcesEnum, string> resourcesUpgradeDesc;
        private Dictionary<int, float> upgradeProgressionDisc;
        private Dictionary<int, float> upgradeCostDisc;


        private void BuildDictionary()
        {
            if (resourcesUpgradeMultiplierDict != null || resourcesUpgradeCostDict != null) return;

            resourcesUpgradeMultiplierDict = new Dictionary<ResourcesEnum, Dictionary<int, float>>();
            resourcesUpgradeCostDict = new Dictionary<ResourcesEnum, Dictionary<int, float>>();
            resourcesUpgradeDesc = new Dictionary<ResourcesEnum, string>();
            upgradeProgressionDisc = new Dictionary<int, float>();
            upgradeCostDisc = new Dictionary<int, float>();

            foreach (UpgradeResources item in upgradeResources)
            {
                foreach (UpgradeLevel item1 in item.upgradeLevel)
                {
                    upgradeProgressionDisc[item1.levelUpgrade] = item1.gainMultiplier;
                    upgradeCostDisc[item1.levelUpgrade] = item1.goldCost;
                }

                resourcesUpgradeMultiplierDict[item.resourcesCategory] = upgradeProgressionDisc;
                resourcesUpgradeCostDict[item.resourcesCategory] = upgradeCostDisc;
                resourcesUpgradeDesc[item.resourcesCategory] = item.descUpgrade;
            }
        }

        public float GetUpgradeLevelMultiplier(ResourcesEnum resourcesCategory, int level)
        {
            BuildDictionary();

            return resourcesUpgradeMultiplierDict[resourcesCategory][level];
        }
        

        public float GetUpgradeLevelCost(ResourcesEnum resourcesCategory, int level)
        {
            BuildDictionary();

            return resourcesUpgradeCostDict[resourcesCategory][level];
        }

        public int GetUpgradeMaxLevel(ResourcesEnum resourcesCategory)
        {
            BuildDictionary();

            return resourcesUpgradeMultiplierDict[resourcesCategory].Values.Count;
        }

        public string GetUpgradeDesc(ResourcesEnum resourcesCategory)
        {
            BuildDictionary();

            return resourcesUpgradeDesc[resourcesCategory]; 
        }

        public List<ResourcesEnum> GetResourcesUpgradeList()
        {
            BuildDictionary();

            List<ResourcesEnum> resourcesEnums = new List<ResourcesEnum>();

            foreach (ResourcesEnum item in resourcesUpgradeMultiplierDict.Keys)
            {
                resourcesEnums.Add(item);
            }

            return resourcesEnums;
        }
    }

    [System.Serializable]
    public class UpgradeResources
    {
        public ResourcesEnum resourcesCategory;
        [TextArea(2, 5)]
        public string descUpgrade;
        public Image iconImage;
        public UpgradeLevel[] upgradeLevel;

    }

    [System.Serializable]
    public class UpgradeLevel
    {
        public int levelUpgrade;
        public float goldCost;
        public float gainMultiplier;
    }
}
