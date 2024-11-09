using System.Collections;
using System.Collections.Generic;
using TvZ.Management;
using UnityEngine;

namespace TvZ.Character
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "Create Asset/ Character/ Create StatSO")]
    public class StatSO : ScriptableObject
    {

        [SerializeField] GameObject charPrefab;
        [SerializeField] public Sprite charImage;
        [SerializeField] public Color bgColor;
        [SerializeField] public AudioClip damageClip;

        [SerializeField] StatCategory[] statCategories;
        [SerializeField] RequiredResourceChar[] requiredResources;

        private Dictionary<StatEnum, float> statTableLookup;
        private Dictionary<ResourcesEnum, float> resourceRequiredTable;

        private void BuildDictionary()
        {
            if (statTableLookup != null) return;

            statTableLookup = new Dictionary<StatEnum, float>();

            foreach ( StatCategory item  in statCategories)
            {
                statTableLookup[item.statEnum] = item.value;
            }
        }

        private void BuildResourceRequiredTable()
        {
            if (resourceRequiredTable != null)return;

            resourceRequiredTable = new Dictionary<ResourcesEnum, float>();

            foreach (RequiredResourceChar item in requiredResources)
            {
                resourceRequiredTable[item.resourceEnum] = item.requiredAmount;
            }
        }

        public float GetCharStat(StatEnum statEnum)
        {
            BuildDictionary();

            return statTableLookup[statEnum];
        }

        public List<ResourcesEnum> GetResoucesListRequired()
        {
            BuildResourceRequiredTable();
            List<ResourcesEnum> resourcesCategoryList = new List<ResourcesEnum>();

            foreach (ResourcesEnum item in resourceRequiredTable.Keys)
            {
                resourcesCategoryList.Add(item);
            }

            return resourcesCategoryList;

        }

        public float GetResourceRequiredAmount(ResourcesEnum resourceEnum)
        {
            BuildResourceRequiredTable();

            return resourceRequiredTable[resourceEnum];
        }


        public GameObject GetCharPrefab() { return charPrefab; }


        
    }


    [System.Serializable]
    public class StatCategory
    {
        public StatEnum statEnum;
        public float value;
    }

    [System.Serializable]
    public class RequiredResourceChar
    {
        public ResourcesEnum resourceEnum;
        public float requiredAmount;
    }
}
