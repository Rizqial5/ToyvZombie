using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Management
{
    public class UnlockableResources : MonoBehaviour
    {
        public static UnlockableResources Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private List<ResourcesEnum> availableResourcesCategory = new List<ResourcesEnum>();

        public bool CheckResourcesCategory(ResourcesEnum category)
        {
            return availableResourcesCategory.Contains(category);
        }

        public void AddResourcesCategpry(ResourcesEnum category)
        {
            availableResourcesCategory.Add(category);
        }
    }
}
