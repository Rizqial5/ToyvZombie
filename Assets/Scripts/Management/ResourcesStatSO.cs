using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Management
{
    [CreateAssetMenu(fileName = "ResourcesSO", menuName ="Create Asset/ Management/ Create ResourcesSO")]
    public class ResourcesStatSO : ScriptableObject
    {
        [SerializeField] ResourceCategory[] resourceCategories;

        private Dictionary<ResourcesEnum, float> resourcesTableLookup;

        private void BuildDictionary()
        {
            if (resourcesTableLookup != null) return;

            resourcesTableLookup = new Dictionary<ResourcesEnum, float>();

            foreach (ResourceCategory resource in resourceCategories)
            {
                resourcesTableLookup[resource.category] = resource.resourcesTotal;
            }
        }

        public float GetResources(ResourcesEnum category)
        {
            BuildDictionary();

            return resourcesTableLookup[category];
        }
    }

    [System.Serializable]
    public class ResourceCategory
    {
        public ResourcesEnum category;
        public float resourcesTotal;
    }

}
