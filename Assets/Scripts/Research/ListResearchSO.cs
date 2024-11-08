using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;
using TvZ.Management;

namespace TvZ.Research
{
    [CreateAssetMenu(fileName = "ListResearchSO", menuName = "Create Asset/ Research/ Create List Research")]
    public class ListResearchSO : ScriptableObject
    {
        [SerializeField] ResearchChar[] listCharResearch;
        [SerializeField] ResearchResource[] listResourceResearch;
        


        private Dictionary<StatSO, Dictionary<ResourcesEnum, float>> researchCharDict;
        private Dictionary<StatSO, string> researchCharDescDict;
        private Dictionary<ResourcesEnum, float> researchCharRequirementDict;

        private Dictionary<ResourcesEnum, ResearchResourceDesc> resourceResearchDict;

        #region Char Research
        private void BuildResearchCharDict()
        {
            if (researchCharDict != null) return;

            researchCharDict = new Dictionary<StatSO, Dictionary<ResourcesEnum, float>>();
            researchCharDescDict = new Dictionary<StatSO, string>();
            researchCharRequirementDict = new Dictionary<ResourcesEnum, float>();

            foreach (ResearchChar item in listCharResearch)
            {

                foreach (ResearchRequirementChar item1 in item.researchRequirements)
                {

                    researchCharRequirementDict[item1.resourcesEnum] = item1.requiredAmount;
                }

                researchCharDict[item.charStat] = researchCharRequirementDict;
                researchCharDescDict[item.charStat] = item.charDesc;
            }


        }

        public List<StatSO> GetListCharResearch()
        {
            List<StatSO> listChar = new List<StatSO>();

            for (int i = 0; i < listCharResearch.Length; i++)
            {
                listChar.Add(listCharResearch[i].charStat);
            }

            return listChar;
        }

        public List<ResourcesEnum> GetCharResearchRequirementCategory(StatSO stat)
        {
            BuildResearchCharDict();

            List<ResourcesEnum> totalRequirementsCategory = new List<ResourcesEnum>();

            foreach (ResourcesEnum item in researchCharDict[stat].Keys)
            {
                totalRequirementsCategory.Add(item);
            }

            return totalRequirementsCategory;
        }

        public float GetCharResearchRequiredAmount(StatSO stat, ResourcesEnum resourcesEnum)
        {
            BuildResearchCharDict();

            return researchCharDict[stat][resourcesEnum];
        }

        public string GetResearchCharDesc(StatSO stat)
        {
            BuildResearchCharDict();

            return researchCharDescDict[stat];
        }

        #endregion


        #region Resource Research
        private void BuildResourceResearchDict()
        { 
            if(resourceResearchDict != null) return;

            resourceResearchDict = new Dictionary<ResourcesEnum, ResearchResourceDesc>();

            foreach (ResearchResource item in listResourceResearch)
            {
                resourceResearchDict[item.resourcesCategroy] = item.researchResourceDesc;
            }

        }

        public List<ResourcesEnum> GetListResourceResearch()
        {
            BuildResourceResearchDict();

            List<ResourcesEnum> resourcesCategoryTotal = new List<ResourcesEnum>();

            foreach (ResourcesEnum item in resourceResearchDict.Keys)
            {
                resourcesCategoryTotal.Add(item);
            }

            return resourcesCategoryTotal;
        }

        public float GetGoldResearchRequirements(ResourcesEnum resourcesEnum)
        {
            BuildResourceResearchDict();

            return resourceResearchDict[resourcesEnum].goldRequirement;
        }

        public string GetResourceDesc(ResourcesEnum resourcesEnum)
        {
            BuildResourceResearchDict();

            return resourceResearchDict[resourcesEnum].resourceFunction;
        }

        #endregion







    }

    [System.Serializable]
    public class ResearchChar
    {
        public StatSO charStat;
        [TextArea(2, 5)]
        public string charDesc;
        public ResearchRequirementChar[] researchRequirements;
    }

    [System.Serializable]
    public class ResearchResource
    {
        public ResourcesEnum resourcesCategroy;
        public ResearchResourceDesc researchResourceDesc;
        

    }

    [System.Serializable]
    public class ResearchResourceDesc
    {
        [TextArea(2, 5)]
        public string resourceFunction;
        public float goldRequirement;
    }

    [System.Serializable]
    public class ResearchRequirementChar
    {
        public ResourcesEnum resourcesEnum;
        public float requiredAmount;
    }
}
