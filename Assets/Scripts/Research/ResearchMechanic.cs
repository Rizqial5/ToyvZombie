using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;
using TvZ.Management;
using UnityEngine.UI;

namespace TvZ.Research
{
    public class ResearchMechanic : MonoBehaviour
    {
        [Header("UI Element")]
        [SerializeField] GameObject researchDescUI;
        [SerializeField] ResearchItem cardResearchPrefab;
        [SerializeField] Transform cardSpawnParent;
        public Button researchButton;

        [Header("Data")]
        [SerializeField] ResourcesStatSO resourcesStatSO;
        [SerializeField] ListResearchSO listResearch;
        

        private List<StatSO> listCharSpawned = new List<StatSO>(); 

        

        
        public void ShowCharList()
        {
            if (listCharSpawned.Count > 0) return;

            listCharSpawned = listResearch.GetListCharResearch();

            for (int i = 0; i < listCharSpawned.Count; i++)
            {
                ResearchItem cardSpawned = Instantiate(cardResearchPrefab, cardSpawnParent);

                List<ResourcesEnum> resourcesEnums = listResearch.GetResearchRequirementCategory(listCharSpawned[i]);

                cardSpawned.GetComponent<Button>().onClick.AddListener(ShowResearchDesc);
                cardSpawned.SetResearchItem(listCharSpawned[i].name, resourcesEnums, resourcesStatSO, listResearch, listCharSpawned[i]);

                

                

            }
        }

        

        public void ShowResearchDesc()
        {
            researchDescUI.SetActive(true);
        }

        public void CloseReasearchDesc()
        {
            researchDescUI.SetActive(false);
        }
    }
}
