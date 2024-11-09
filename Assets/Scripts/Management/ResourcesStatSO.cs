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
        private Dictionary<ResourcesEnum, Sprite> resourcesImageLookup;

        private void BuildDictionary()
        {
            if (resourcesTableLookup != null) return;

            resourcesTableLookup = new Dictionary<ResourcesEnum, float>();
            resourcesImageLookup = new Dictionary<ResourcesEnum, Sprite>();

            foreach (ResourceCategory resource in resourceCategories)
            {
                resourcesTableLookup[resource.category] = resource.resourcesTotal;
                resourcesImageLookup[resource.category] = resource.resourcesImage;
            }
        }

        public float GetResources(ResourcesEnum category)
        {
            BuildDictionary();

            return resourcesTableLookup[category];
        }

        public Sprite GetImage(ResourcesEnum category)
        {
            BuildDictionary();

            return resourcesImageLookup[category];
        }

        public void AddResources(ResourcesEnum category, float amount)
        {
            BuildDictionary();

            resourcesTableLookup[category] += amount;
        }
    }

    [System.Serializable]
    public class ResourceCategory
    {
        public ResourcesEnum category;
        public Sprite resourcesImage;
        public float resourcesTotal;
    }

}
