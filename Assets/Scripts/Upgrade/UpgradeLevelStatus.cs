using System.Collections;
using System.Collections.Generic;
using TvZ.Management;
using UnityEngine;

namespace TvZ.Upgrade
{
    public class UpgradeLevelStatus : MonoBehaviour
    {
        
        [SerializeField] UpgradableResources[] upgradableResources;
        

        private Dictionary<ResourcesEnum,int> upgradeableResourcesDict = new Dictionary<ResourcesEnum,int>();

        private void Start()
        {
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            foreach (UpgradableResources item in upgradableResources)
            {
                upgradeableResourcesDict[item.ResourcesEnum] = item.levelUpgradeNow; 
            }
        }

        public int GetLevelResourcesNow(ResourcesEnum resourcesEnum)
        {
            return upgradeableResourcesDict[resourcesEnum];
        }

        public void SetLevelResources(ResourcesEnum resourcesEnum, int setLevelUpgrade)
        {
            upgradeableResourcesDict[resourcesEnum] = setLevelUpgrade;
        }

        
    }

    [System.Serializable]
    public class UpgradableResources
    {
        public ResourcesEnum ResourcesEnum;
        public int levelUpgradeNow;
    }
}
