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


        private Dictionary<StatSO, Dictionary<ResourcesEnum, float>> researchCharDict;

        private Dictionary<ResourcesEnum, float> researchRequirementDict;


        private void BuildResearchDict()
        {
            if (researchCharDict != null) return;

            researchCharDict = new Dictionary<StatSO, Dictionary<ResourcesEnum, float>>();
            researchRequirementDict = new Dictionary<ResourcesEnum, float>();

            foreach (ResearchChar item in listCharResearch)
            {
                
                foreach (ResearchRequirement item1 in item.researchRequirements)
                {
                    
                    researchRequirementDict[item1.resourcesEnum] = item1.requiredAmount;
                }

                researchCharDict[item.charStat] = researchRequirementDict;
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
        
        public List<ResourcesEnum> GetResearchRequirementCategory(StatSO stat)
        {
            BuildResearchDict();

            List<ResourcesEnum> totalRequirementsCategory = new List<ResourcesEnum>();

            foreach (ResourcesEnum item in researchCharDict[stat].Keys)
            {
                totalRequirementsCategory.Add(item);
            }

            return totalRequirementsCategory;
        }

        public float GetResearchRequiredAmount(StatSO stat, ResourcesEnum resourcesEnum)
        {
            BuildResearchDict();

            return researchCharDict[stat][resourcesEnum];
        }

        
        
    }

    [System.Serializable]
    public class ResearchChar
    {
        public StatSO charStat;
        public ResearchRequirement[] researchRequirements;
    }

    [System.Serializable]
    public class ResearchRequirement
    {
        public ResourcesEnum resourcesEnum;
        public float requiredAmount;
    }
}
