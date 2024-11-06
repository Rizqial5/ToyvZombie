using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Management;
using TvZ.Character;

namespace TvZ.Enemy
{
    public class DropMechanic : MonoBehaviour
    {
        [SerializeField] ResourcesStatSO resourcesStatSO;

        public ResourcesDrop[] resourcesDrop;

        private CharStat charStat;
        private ResourcesInventoryUI inventoryUI;

        private void Awake()
        {
            charStat = GetComponent<CharStat>();
        }

        private void Start()
        {
            charStat.onCharDie.AddListener(DropItem);

            inventoryUI = FindAnyObjectByType<ResourcesInventoryUI>();
        }

        public void DropItem()
        {

            if (resourcesDrop == null) return;

            for (int i = 0; i < resourcesDrop.Length; i++)
            {
                resourcesStatSO.AddResources(resourcesDrop[i].resourcesEnum, resourcesDrop[i].amountDrop);

                

                inventoryUI.ShowResources();
            }
        }


    }

    [System.Serializable]
    public class ResourcesDrop
    {
        public ResourcesEnum resourcesEnum;
        public float amountDrop;
    }
}
